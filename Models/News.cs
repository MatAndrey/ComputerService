using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public ICollection<NewsTranslation> Translations { get; set; }
    }
}
