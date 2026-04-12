using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class NewsTranslation
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; } 

        [Required, MaxLength(10)]
        public string LangCode { get; set; }

        public int NewsId { get; set; }

        [ForeignKey(nameof(NewsId))]
        public News News { get; set; }
    }
}