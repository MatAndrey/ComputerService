using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, MaxLength(255)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}