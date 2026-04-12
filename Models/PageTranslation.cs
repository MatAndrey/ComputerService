using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class PageTranslation
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, MaxLength(10)]
        public string LangCode { get; set; }

        public int PageId { get; set; }

        [ForeignKey(nameof(PageId))]
        public Page Page { get; set; }
    }
}