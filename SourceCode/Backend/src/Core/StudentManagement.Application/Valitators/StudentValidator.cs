using FluentValidation;
using StudentManagement.Core.Application.DTOs;

namespace StudentManagement.Core.Application.Valitators
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Now.AddYears(-6))
                .WithMessage("Students cannot be younger than 6 years old.");
        }
    }
}