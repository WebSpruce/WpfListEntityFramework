namespace entityFramework_2WPF.Models
{
    public class OrderDetail
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }
        public Order? Orders { get; set; } = null!;
        public int? ProductId { get; set; }
        public Product? Products { get; set; } = null!;
    }
}