using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ComputerService.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
