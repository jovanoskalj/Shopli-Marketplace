using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain_Models
{
    public class Product : BaseEntity
    {
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = default!;

        [Required]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; } = default!;

        [Required]
        [Display(Name = "Image URL")]
        public string ProductImage { get; set; } = default!;

        [Required]
        [Display(Name = "Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double ProductPrice { get; set; }

        [Display(Name = "Discounted Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Discounted price cannot be negative")]
        public double? DiscountedPrice { get; set; }

        [Required]
        [Display(Name = "Rating")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double Rating { get; set; }

        [Display(Name = "Stock Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; } = 0;

        [Display(Name = "SKU")]
        public string? SKU { get; set; }

        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        [Display(Name = "Weight (kg)")]
        [Range(0, double.MaxValue, ErrorMessage = "Weight cannot be negative")]
        public double? Weight { get; set; }

        [Display(Name = "Dimensions")]
        public string? Dimensions { get; set; }

        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "View Count")]
        public int ViewCount { get; set; } = 0;

        [Display(Name = "Tags")]
        public string? Tags { get; set; }

        [Display(Name = "Meta Description")]
        public string? MetaDescription { get; set; }

        // Foreign Keys
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        // Navigation Properties
        public virtual ICollection<ProductInShoppingCart>? AllShoppingCarts { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Wishlist>? WishlistItems { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }

        // Computed Properties
        public double? DisplayPrice => DiscountedPrice ?? ProductPrice;
        public bool HasDiscount => DiscountedPrice.HasValue && DiscountedPrice < ProductPrice;
        public double? DiscountPercentage => HasDiscount ? Math.Round((1 - (DiscountedPrice!.Value / ProductPrice)) * 100, 2) : null;
        public bool IsInStock => StockQuantity > 0;
        public int ReviewCount => Reviews?.Count ?? 0;
    }
}
