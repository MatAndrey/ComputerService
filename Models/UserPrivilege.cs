using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class UserPrivilege
    {
        [Required, MaxLength(50)]
        public string UserLogin { get; set; }

        [Required, MaxLength(100)]
        public string PrivilegeName { get; set; }

        [ForeignKey(nameof(UserLogin))]
        public User User { get; set; }
    }
}