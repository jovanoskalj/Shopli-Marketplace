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
        public double ProductPrice { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public double Rating { get; set; }

        public virtual ICollection<ProductInShoppingCart>? AllShoppingCarts { get; set; }
    }

}
