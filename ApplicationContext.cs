using CSV_Reader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSV_Reader
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Cab> Cabs => Set<Cab>();

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cab>()
            .HasKey(c => new { c.PickUpLocationId, c.DropOffLocationId, c.TripDistance, c.PassengerCount, c.PickUpTime, c.DropOffTime, c.TipAmount, c.FareAmount });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=XIAOMI\SQLEXPRESS;Database=CSV_Data;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

}
