using EShop.Domain.Identity_Models;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Domain_Models
{
    public class Wishlist : BaseEntity
    {
        [Required]
        public string UserId { get; set; } = default!;
        public virtual EShopApplicationUser User { get; set; } = default!;

        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = default!;

        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}
