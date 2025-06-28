using EShop.Domain.Domain_Models;
using EShop.Domain.Identity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<ProductInOrder> ProductInOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure unique constraints
            modelBuilder.Entity<Wishlist>()
                .HasIndex(w => new { w.UserId, w.ProductId })
                .IsUnique();

            // Configure decimal precision for prices
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountedPrice)
                .HasPrecision(18, 2);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            var categories = new[]
            {
                new Category { Id = Guid.NewGuid(), Name = "Electronics", Description = "Electronic devices and gadgets", IconCssClass = "fas fa-laptop", DisplayOrder = 1, IsActive = true },
                new Category { Id = Guid.NewGuid(), Name = "Clothing", Description = "Fashion and apparel", IconCssClass = "fas fa-tshirt", DisplayOrder = 2, IsActive = true },
                new Category { Id = Guid.NewGuid(), Name = "Books", Description = "Books and literature", IconCssClass = "fas fa-book", DisplayOrder = 3, IsActive = true },
                new Category { Id = Guid.NewGuid(), Name = "Home & Garden", Description = "Home improvement and gardening", IconCssClass = "fas fa-home", DisplayOrder = 4, IsActive = true },
                new Category { Id = Guid.NewGuid(), Name = "Sports", Description = "Sports and outdoor activities", IconCssClass = "fas fa-football-ball", DisplayOrder = 5, IsActive = true }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
