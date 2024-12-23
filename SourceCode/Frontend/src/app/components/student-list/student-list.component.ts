import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentService } from '../../services/student.service';
import { Student } from '../../models/student';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-list',
  imports: [CommonModule],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.scss'
})
export class StudentListComponent implements OnInit{
  students: Student[] = [];

  constructor(private router: Router, private studentService: StudentService) { }

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents(): void {
    this.studentService.getAll().subscribe({
      next: (data) => this.students = data,
      error: (error) => console.error('Error loading students:', error)
    });
  }

  openAddForm(): void {
    this.router.navigate(['/students/add']);
  }

  editStudent(student: Student): void {
    this.router.navigate(['/students/edit', student.id]);
  }

  deleteStudent(id: number): void {
    if (confirm('Are you sure you want to delete this student?')) {
      this.studentService.delete(id).subscribe({
        next: () => {
          this.loadStudents();
          // Optional: Add a success message
        },
        error: (error) => {
          console.error('Error deleting student:', error);
          // Optional: Add error handling UI
        }
      });
    }
  }
}
