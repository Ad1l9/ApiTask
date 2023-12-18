namespace ApiTask.Repositories.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
