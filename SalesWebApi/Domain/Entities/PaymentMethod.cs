namespace Domain.Entities
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
