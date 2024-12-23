import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { CommonModule } from '@angular/common';
import { FluentValidationError } from '../../models/FluentValidationError';

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './student-form.component.html'
})
export class StudentFormComponent implements OnInit {
  studentForm: FormGroup;
  submitted = false;
  errorMessage: string = '';
  isEditMode = false;
  studentId?: number;

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private router: Router,
    private route: ActivatedRoute
  ) {
      this.studentForm = this.fb.group({
        firstName: ['', [Validators.required, Validators.minLength(2)]],
        lastName: ['', [Validators.required, Validators.minLength(2)]],
        email: ['', [Validators.required, Validators.email]],
        dateOfBirth: ['', Validators.required]
      });
  }

  ngOnInit(): void {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
    this.isEditMode = !!this.studentId;

    if (this.isEditMode && this.studentId) {
      this.loadStudent(this.studentId);
    }
  }

  private loadStudent(id: number): void {
    this.studentService.getById(id).subscribe({
      next: (student) => {
        // Format date for input (YYYY-MM-DD)
        const dateOfBirth = new Date(student.dateOfBirth)
                                    .toISOString()
                                    .split('T')
                                    [0];
        
        this.studentForm.patchValue({
          firstName: student.firstName,
          lastName: student.lastName,
          email: student.email,
          dateOfBirth: dateOfBirth
        });
      },
      error: (error) => {
        this.errorMessage = 'Error loading student details';
        console.error('Error:', error);
      }
    });
  }

  get f() { 
    return this.studentForm.controls as {
      [key: string]: AbstractControl;
    }; 
  }
  
  onSubmit(): void {
    this.submitted = true;
    this.errorMessage = '';

    if (this.studentForm.invalid) {
      return;
    }

    const operation = this.isEditMode
      ? this.studentService.update({ id: this.studentId!, ...this.studentForm.value })
      : this.studentService.create(this.studentForm.value);

    operation.subscribe({
      next: () => {
        this.router.navigate(['/students']);
      },
      error: (error) => {
        if (error.status === 400) {
          if (Array.isArray(error.error)) {
            this.errorMessage = (error.error as FluentValidationError[])
              .map(err => err.errorMessage)
              .join(', ');
          } else {
            this.errorMessage = 'Invalid data submitted';
          }
        } else {
          this.errorMessage = 'An error occurred while saving the student';
        }
      }
    });
  }
  
  onCancel(): void {
    this.router.navigate(['/students']);
  }
}