using System.ComponentModel.DataAnnotations;

namespace ComputerService.ViewModels
{
    public class SupportMessageViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
