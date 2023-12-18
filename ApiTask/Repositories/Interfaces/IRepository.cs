using System.Linq.Expressions;

namespace ApiTask.Repositories.Interfaces
{
    public interface IRepository<T> where T: BaseEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            params string[] includes);

        IQueryable<T> GetAllAsync(
            Expression<Func<T, object>>? orderExpression = null,
            int skip = 0, int take = 0,
            bool isTracking = true,
            bool isDescending = false,
            params string[] includes);
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        void Update(T entity);
        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
