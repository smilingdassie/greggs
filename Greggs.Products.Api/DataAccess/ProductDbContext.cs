using Microsoft.EntityFrameworkCore;
using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Greggs.Products.Api.DataAccess
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Sausage Roll", PriceInPounds = 1m },
                new Product { Id = 2, Name = "Vegan Sausage Roll", PriceInPounds = 1.1m },
                new Product { Id = 3, Name = "Steak Bake", PriceInPounds = 1.2m },
                new Product { Id = 4, Name = "Yum Yum", PriceInPounds = 0.7m },
                new Product { Id = 5, Name = "Pink Jammie", PriceInPounds = 0.5m },
                new Product { Id = 6, Name = "Mexican Baguette", PriceInPounds = 2.1m },
                new Product { Id = 7, Name = "Bacon Sandwich", PriceInPounds = 1.95m },
                new Product { Id = 8, Name = "Coca Cola", PriceInPounds = 1.2m }
            );
        }
    }
}
