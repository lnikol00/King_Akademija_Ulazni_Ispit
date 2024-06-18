using Microsoft.EntityFrameworkCore;

namespace DEV_Test.Services.ProductService.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<SearchParams> Searches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchParams>().HasKey(nameof(SearchParams.Search));

            base.OnModelCreating(modelBuilder);
        }
    }
}
