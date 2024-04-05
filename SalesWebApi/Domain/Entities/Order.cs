namespace Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Color> Colors { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


    }
}
