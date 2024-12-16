namespace StudentManagement.Core.Application.Contracts.IRepository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}