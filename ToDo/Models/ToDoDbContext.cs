using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace ToDo.Models
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> ToDos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category> ().HasData(
                               new Category { CategoryId = "home", Name = "Home" },
                               new Category { CategoryId = "work", Name = "Work" },
                               new Category { CategoryId = "hobby", Name = "Hobby" },
                               new Category { CategoryId = "shop", Name = "Shop" }
                               );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", Name = "Open" },
                new Status { StatusId = "closed", Name = "Closed" }
                );

            modelBuilder.Entity<User>().HasData(
                               new User { UserId = "jack", Name = "Jack" },
                               new User { UserId = "harry", Name = "Harry" },
                               new User { UserId = "william", Name = "William" }
                               );
        }
    }
}
