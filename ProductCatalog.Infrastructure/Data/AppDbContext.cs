using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .HasMany(p => p.Categories)
                        .WithMany();

            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}

