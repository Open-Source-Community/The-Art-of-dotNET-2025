using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Metrics;
using WebApplication2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication2.Data
{
    public class AppDbcontext: DbContext

    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        public AppDbcontext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = sandt; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
            #endregion
        }

        public DbSet<Customers> customers {  get; set; }

        #region
        private List<Customers> GenerateSampleCustomers(int count)

        {
            return Enumerable.Range(1, count).Select(i => new Customers
            {
                Id = i,
                Name = $"First{i}",
                Email = $"customer{i}@example.com",
                PhoneNumber = $"123-456-{1000 + i}",
            }).ToList();
        }
        #endregion

        #region
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region
           modelBuilder.Entity<Customers>().HasData(GenerateSampleCustomers(10));
            #endregion
        }
        #endregion

    }
}
