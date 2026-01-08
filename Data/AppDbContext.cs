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
    }
}
