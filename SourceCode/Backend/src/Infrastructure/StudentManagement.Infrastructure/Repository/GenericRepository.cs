using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.Application.Contracts.IRepository;
using StudentManagement.Core.Domain.Common;

namespace StudentManagement.Infrastructure.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                context.Set<T>().Remove(entity);
        }
    }
}