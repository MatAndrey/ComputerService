using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        public ICollection<NewsTranslation> Translations { get; set; }
    }
}
