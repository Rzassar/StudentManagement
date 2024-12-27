using Microsoft.AspNetCore.Mvc;
using StudentManagement.Core.Application.Contracts.IRepository;
using StudentManagement.Core.Application.DTOs;
using StudentManagement.Core.Application.Valitators;

namespace StudentManagement.UI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        #region Fields & Properties

        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<StudentController> logger;

        #endregion

        #region Constructors

        public StudentController(IUnitOfWork unitOfWork, ILogger<StudentController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await unitOfWork.Students.GetAllAsync();
            return Ok(students.Select(StudentDto.FromStudent));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await unitOfWork.Students.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(StudentDto.FromStudent(student));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] StudentValidator validator, [FromBody] StudentDto studentDto)
        {
            var validationResult = await validator.ValidateAsync(studentDto);

            if (!validationResult.IsValid)
            {
                logger.LogWarning("Date and Time {@DateTime} - Validation failed: {@ValidationErrors}", 
                                    validationResult.Errors, 
                                    DateTime.Now);
                return BadRequest(validationResult.Errors);
            }

            var student = studentDto.ToStudent();
            unitOfWork.Students.Add(student);

            await unitOfWork.SaveChangesAsync();

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromServices] StudentValidator validator, [FromBody] StudentDto studentDto)
        {
            var validationResult = await validator.ValidateAsync(studentDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            unitOfWork.Students.Update(studentDto.ToStudent());

            await unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await unitOfWork.Students.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        #endregion
    }
}