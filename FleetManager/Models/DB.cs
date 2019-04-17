using Microsoft.EntityFrameworkCore;

namespace FleetManager.Models{
    class CarsDbContext : DbContext {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
        : base(options) { }

        public DbSet<Car> Car { get; set; }
        /* public DbSet<Modal> Modal { get; set; }
        public DbSet<Motor> Motor { get; set; }
        public DbSet<Brand> Brand { get; set; } */
        // DbSet<T> type properties for other domain models
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Motor)
                .WithOne(a => a.Car)
                .HasForeignKey<Car>(a => a.Regno)
                .HasPrincipalKey<Motor>(c => c.ID);
        } */
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