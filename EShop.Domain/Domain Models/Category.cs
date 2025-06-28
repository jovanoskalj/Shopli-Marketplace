using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Domain_Models
{
    public class Category : BaseEntity
    {
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = default!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Icon CSS Class")]
        public string? IconCssClass { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; } = 0;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<Product>? Products { get; set; }
    }
}
