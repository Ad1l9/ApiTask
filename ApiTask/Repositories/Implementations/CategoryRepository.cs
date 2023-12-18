using ApiTask.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ApiTask.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {

        }
    }
}
