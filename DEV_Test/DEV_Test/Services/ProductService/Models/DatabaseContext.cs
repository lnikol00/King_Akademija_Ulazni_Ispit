using Microsoft.EntityFrameworkCore;

namespace DEV_Test.Services.ProductService.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<SearchParams> Searches { get; set; }
        public DbSet<FilterParams> Filters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchParams>().HasKey(nameof(SearchParams.Search));
            modelBuilder.Entity<FilterParams>().HasKey(nameof(FilterParams.Order), nameof(FilterParams.Category));

            base.OnModelCreating(modelBuilder);
        }
    }
}
