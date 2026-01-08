using CFA_EFC_WEPAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CFA_EFC_WEPAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        //Product - Database - Singular form
        //Products - .NET - Plural form
        //DbSet<Product> Products { get; set; }
        //DbSet represents a collection of entities of a specific type - Product in this case
        //Entity represents a table in the database
        public DbSet<Product> Product => Set<Product>();
        public DbSet<Users> User => Set<Users>();
        public DbSet<Employee> Employees => Set<Employee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Data Seeding for Users
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    UserName = "admin",
                    Email = "admin@demo.com",
                    Role = "Admin"
                },
                new Users
                {
                    Id = 2,
                    UserName = "john",
                    Email = "john@demo.com",
                    Role = "User"
                }
            );
            //Data Seeding for Employees
            //You can assign values from Code First approach
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    UserName = "jane",
                    Email = "jane@demo.com",
                },
                new Employee
                {
                    Id = 2,
                    UserName = "john",
                    Email = "john@demo.com",
                }
            );
        }
    }
}
