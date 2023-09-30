using System.Collections.Generic;


namespace entityFramework_2WPF.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<Product>? Products { get; set; }
        public ICollection<CartProduct>? CartProducts { get; set; }
    }
}
