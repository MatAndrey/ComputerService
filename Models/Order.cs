using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerService.Models
{
    public class Order
    {
        [Key]
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

        [MaxLength(20)]
        public string Status { get; set; } = "new";

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
