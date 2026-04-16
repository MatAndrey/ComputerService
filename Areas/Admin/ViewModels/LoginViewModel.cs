using System.ComponentModel.DataAnnotations;

namespace ComputerService.Areas.Admin.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
