using StudentManagement.Core.Application.Contracts.IRepository.IRepositories;

namespace StudentManagement.Core.Application.Contracts.IRepository
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }

        Task<int> SaveChangesAsync();
    }
}