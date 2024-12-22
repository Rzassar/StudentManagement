using StudentManagement.Core.Application.Contracts.IRepository;
using StudentManagement.Core.Application.Contracts.IRepository.IRepositories;
using StudentManagement.Infrastructure.Repository.Repositories;

namespace StudentManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IStudentRepository students;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IStudentRepository Students
        {
            get
            {
                students ??= new StudentRepository(context);
                return students;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}