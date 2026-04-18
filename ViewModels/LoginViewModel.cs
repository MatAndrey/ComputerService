using System.ComponentModel.DataAnnotations;

namespace ComputerService.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
