namespace ApiControllerMethodsPractise.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal Price { get; set; }
        public int Availability { get; set; }
        public bool isAvailable { get; set; }
    }
}
