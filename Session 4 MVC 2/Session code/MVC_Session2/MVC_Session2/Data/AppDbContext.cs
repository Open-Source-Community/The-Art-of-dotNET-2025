using Microsoft.EntityFrameworkCore;
using MVC_Session2.Models;

namespace MVC_Session2.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Session4;Integrated Security=True;Encrypt=False;Trust Server Certificate=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.CategoryId);
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
