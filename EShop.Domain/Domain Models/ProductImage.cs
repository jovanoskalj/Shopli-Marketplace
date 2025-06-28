using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Domain_Models
{
    public class ProductImage : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = default!;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = default!;

        [Display(Name = "Alt Text")]
        public string? AltText { get; set; }

        [Display(Name = "Is Primary")]
        public bool IsPrimary { get; set; } = false;

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; } = 0;
    }
}
