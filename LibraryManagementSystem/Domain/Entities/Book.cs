namespace Domain.Entities
{
    public class Book
    {
        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public int BookYear { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; }
        public int BookPagesCount { get; set; }
        public int BookInventoryCount { get; set; }
        public int BookItemsInStock { get; set; }
        public bool BookIsAvailabe { get; set; }
    }
}
