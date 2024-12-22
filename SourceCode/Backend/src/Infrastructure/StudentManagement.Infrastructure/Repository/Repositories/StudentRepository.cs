using StudentManagement.Core.Application.Contracts.IRepository.IRepositories;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Repository.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}