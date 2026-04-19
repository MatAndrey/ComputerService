using System.ComponentModel.DataAnnotations;

namespace ComputerService.ViewModels
{
    public class ReviewViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string AuthorName { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public string Text { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
