using inventory.domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace inventory.infrastructure
{

    public class InventoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Title)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Title)
                .HasMaxLength(40);

            modelBuilder.Entity<User>().HasData(
                new User("Ehsan"),
                new User ("Niki") 
            );
        }
    }

}
