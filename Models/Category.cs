using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public ICollection<CategoryTranslation> Translations { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
