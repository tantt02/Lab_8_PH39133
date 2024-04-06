using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai3.Models
{
    public class Carts
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int IdProuducts { get; set; }

        [ForeignKey("IdProuducts")]

        public Products? Products { get; set; }
    }
}
