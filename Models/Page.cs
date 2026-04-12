using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }

        public ICollection<PageTranslation> Translations { get; set; }
    }
}
