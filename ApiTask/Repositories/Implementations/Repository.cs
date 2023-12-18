using ApiTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiTask.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity,new()
    {
        private readonly DbSet<T> _table;
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAllAsync(
            Expression<Func<T, object>>? orderExpression = null,
            int skip = 0, int take = 0,
            bool isTracking = true,
            bool isDescending = false, params string[] includes)
        {
            var query = _table.AsQueryable();


            if (orderExpression is not null)
            {
                if (isDescending) query = query.OrderByDescending(orderExpression);

                else query = query.OrderBy(orderExpression);
            }

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if(skip > 0) query = query.Skip(skip);

            if (take > 0)  query = query.Take(take);

            return isTracking?query:query.AsNoTracking();
        }


        public async Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            params string[] includes)
        {
            var query = _table.AsQueryable();
            
            
            if(expression is not null)  query = query.Where(expression);
            

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(c => c.Id == id);

            return entity;
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
