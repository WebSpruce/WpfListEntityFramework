using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace entityFramework_2WPF.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = null!;
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public byte[]? ImageData { get; set; }
        public ICollection<OrderDetailProduct>? OrderDetailsProducts { get; set; }
        public ICollection<CartProduct>? CartProducts { get; set; }
    }
}
