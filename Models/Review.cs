using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class Review
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
