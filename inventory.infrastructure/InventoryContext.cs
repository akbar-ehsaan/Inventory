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
            modelBuilder.Entity<User>()
                 .HasIndex(u => u.Name)
                 .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Title)
                .IsUnique();

            // Configure User and Orders relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Buyer)
                .HasForeignKey(o => o.BuyerId)  // Make sure to use BuyerId here
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Order and Product relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()  // No navigation property on Product side
                .HasForeignKey(o => o.ProductId)  // Make sure to use ProductId here
                .HasForeignKey(o => o.BuyerId);  // Make sure to use ProductId here

            modelBuilder.Entity<Order>()
                .Property(o => o.CreationDate)
                .HasDefaultValueSql("GETUTCDATE()");


            modelBuilder.Entity<User>().HasData(
                new User("Ehsan"),
                new User ("Niki") 
            );
        }
    }

}
