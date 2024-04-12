namespace Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? ColorId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price{ get; set; }
        public int Availability { get; set; }
        public bool isAvailable { get; set; }
        public ICollection <Color> Colors { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
