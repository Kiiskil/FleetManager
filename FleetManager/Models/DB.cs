using Microsoft.EntityFrameworkCore;

namespace FleetManager.Models{
    class CarsDbContext : DbContext {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
        : base(options) { }

        public DbSet<Car> Car { get; set; }
        // DbSet<T> type properties for other domain models
    }

    class CarsDbContextFactory {
        public static CarsDbContext Create(string connectionString) {
            var optionsBuilder = new DbContextOptionsBuilder<CarsDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            var CarsDbContext = new CarsDbContext(optionsBuilder.Options);
            return CarsDbContext;
        }
    }
}