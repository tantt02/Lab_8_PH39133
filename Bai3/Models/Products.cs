using System.ComponentModel.DataAnnotations;

namespace Bai3.Models
{
    public class Products
    {
        [Key]
        public int IdProduct { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public bool Status { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }
        public ICollection<Carts>? Cart {  get; set; }
    }
}
