using CSV_Reader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSV_Reader
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Taxi> Cabs => Set<Taxi>();

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=XIAOMI\SQLEXPRESS;Database=CSV_Data;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

}
