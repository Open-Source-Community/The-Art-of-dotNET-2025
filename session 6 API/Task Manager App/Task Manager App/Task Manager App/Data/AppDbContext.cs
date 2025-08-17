using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Task_Manager.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configure the database connection here if not already configured
                optionsBuilder.UseSqlServer("YourConnectionStringHere");
            }
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between User and Task
            modelBuilder.Entity<User>()
                //.ToTable("Users")
                .HasMany(u => u.Tasks)
                .WithOne(t => t.Creator)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete tasks when a user is deleted

            modelBuilder.Entity<TodoTask>()
                .ToTable("Tasks");

            // Ensure Email is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
