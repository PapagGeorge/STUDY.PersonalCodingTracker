namespace Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int customerId { get; set; }
        public Customer Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<Product> Products { get; set; }


    }
}
