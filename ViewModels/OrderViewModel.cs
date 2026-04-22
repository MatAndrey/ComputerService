using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required, MaxLength(150)]
        public string CustomerName { get; set; }

        [Required, MaxLength(30)]
        public string Phone { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string PaymentMethod { get; set; }

        [MaxLength(100)]
        public string DeliveryMethod { get; set; }

        [MaxLength(255)]
        public string DeliveryAddress { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [ValidateNever]
        public List<OrderItemViewModel> Items { get; set; }
    }
}
