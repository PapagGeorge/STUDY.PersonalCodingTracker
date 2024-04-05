namespace Domain.Entities
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
