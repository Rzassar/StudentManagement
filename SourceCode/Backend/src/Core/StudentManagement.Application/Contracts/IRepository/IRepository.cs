using StudentManagement.Core.Domain.Common;

namespace StudentManagement.Core.Application.Contracts.IRepository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<TEntity> AddAsync(TEntity entity);
        
        Task UpdateAsync(TEntity entity);
        
        Task DeleteAsync(int id);
    }
}