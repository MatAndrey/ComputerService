using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class CategoryTranslation
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(10)]
        public string LangCode { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}