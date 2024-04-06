using System.ComponentModel.DataAnnotations;

namespace Bai2.Models
{
    public class Customers
    {

        [Key]
        public Guid IdCustomers { get; set; }
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]

        public string PhoneNumber { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Address { get; set; } = "";
        [Required]
        public bool Sex { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
