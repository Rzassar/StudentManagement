using StudentManagement.Core.Domain.Common;

namespace StudentManagement.Domain.Entities
{
    public class Student : EntityBase
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
    }
}