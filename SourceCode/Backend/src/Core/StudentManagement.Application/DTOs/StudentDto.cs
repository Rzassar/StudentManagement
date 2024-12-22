using StudentManagement.Domain.Entities;

namespace StudentManagement.Core.Application.DTOs
{
    public record StudentDto(int Id,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Email)
    {
        public static StudentDto FromStudent(Student student)
        {
            return new StudentDto(student.Id,
                                    student.FirstName,
                                    student.LastName,
                                    student.DateOfBirth,
                                    student.Email);
        }

        public Student ToStudent()
        {
            return new Student
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Email = Email
            };
        }
    }
}