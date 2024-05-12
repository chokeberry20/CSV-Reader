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
            //Change this to your conncetion stirng
            optionsBuilder.UseSqlServer(@"Server= Your Server ;Database=CSV_Data;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

}
