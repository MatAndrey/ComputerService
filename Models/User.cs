using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class User
    {
        [Key, MaxLength(50)]
        public string Login { get; set; }

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        public ICollection<UserPrivilege> UserPrivileges { get; set; }
    }
}
