using EShop.Domain.Identity_Models;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Domain_Models
{
    public class Review : BaseEntity
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Display(Name = "Review Title")]
        [MaxLength(200)]
        public string? Title { get; set; }

        [Display(Name = "Review Comment")]
        [MaxLength(1000)]
        public string? Comment { get; set; }

        [Display(Name = "Review Date")]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Is Verified Purchase")]
        public bool IsVerifiedPurchase { get; set; } = false;

        [Display(Name = "Helpful Count")]
        public int HelpfulCount { get; set; } = 0;

        // Foreign Keys
        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;
        public virtual EShopApplicationUser User { get; set; } = default!;
    }
}
