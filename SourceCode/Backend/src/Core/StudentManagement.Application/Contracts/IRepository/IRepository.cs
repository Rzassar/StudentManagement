using StudentManagement.Core.Domain.Common;

namespace StudentManagement.Core.Application.Contracts.IRepository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        
        void Add(TEntity entity);
        
        void Update(TEntity entity);
        
        Task DeleteAsync(int id);
    }
}