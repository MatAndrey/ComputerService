using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
