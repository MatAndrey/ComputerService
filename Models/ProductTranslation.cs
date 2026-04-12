using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class ProductTranslation
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required, MaxLength(10)]
        public string LangCode { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}