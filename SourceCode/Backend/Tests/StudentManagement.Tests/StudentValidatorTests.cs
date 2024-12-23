using StudentManagement.Core.Application.DTOs;
using StudentManagement.Core.Application.Valitators;

namespace StudentManagement.Tests
{
    public class StudentValidatorTests
    {
        private readonly StudentValidator validator;

        public StudentValidatorTests()
        {
            validator = new StudentValidator();
        }

        [Fact]
        public async Task Validate_WithValidStudent_ShouldPass()
        {
            // Arrange
            var studentDto = new StudentDto(0, "John", "Doe",
                DateTime.Now.AddYears(-20), "john@example.com");

            // Act
            var result = await validator.ValidateAsync(studentDto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("", "Doe", "Invalid FirstName")]
        [InlineData("John", "", "Invalid LastName")]
        [InlineData("John", "Doe", "invalid-email")]
        public async Task Validate_WithInvalidData_ShouldFail(
            string firstName, string lastName, string email)
        {
            // Arrange
            var studentDto = new StudentDto(0, firstName, lastName,
                DateTime.Now.AddYears(-20), email);

            // Act
            var result = await validator.ValidateAsync(studentDto);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}