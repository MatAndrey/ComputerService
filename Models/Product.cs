using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public bool Visible { get; set; }

        public ICollection<ProductTranslation> Translations { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}