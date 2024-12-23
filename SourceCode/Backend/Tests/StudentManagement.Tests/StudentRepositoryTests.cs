using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.Application.Contracts.IRepository;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure;
using StudentManagement.Infrastructure.Repository.Repositories;

namespace StudentManagement.Tests
{
    public class StudentRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        private readonly ApplicationDbContext context;
        private readonly IRepository<Student> repository;

        public StudentRepositoryTests()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            repository = new StudentRepository(context);
        }

        [Fact]
        public async Task Add_ShouldAddNewStudent()
        {
            // Arrange
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            // Act
            repository.Add(student);
            await context.SaveChangesAsync();

            // Assert
            var addedStudent = await context.Set<Student>().FindAsync(student.Id);
            Assert.NotNull(addedStudent);
            Assert.Equal("John", addedStudent.FirstName);
        }
    }
}