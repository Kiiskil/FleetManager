using Microsoft.EntityFrameworkCore;

namespace FleetManager.Models{
    class CarsDbContext : DbContext {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
        : base(options) { }

        public DbSet<Car> Car { get; set; }
        public DbSet<Model> CarModel { get; set; }
        public DbSet<Motor> Motor { get; set; }
        public DbSet<Brand> Brand { get; set; }
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