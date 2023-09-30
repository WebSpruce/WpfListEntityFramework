using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityFramework_2WPF.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
