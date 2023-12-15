using ApiTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Tag>().Property(t => t.Name).IsRequired().HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }


    }
}
