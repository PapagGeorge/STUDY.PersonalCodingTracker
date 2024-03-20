namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserMobilePhone { get; set; }
        public string UserAddress { get; set; }
        public int UserNumberOfBooksRented { get; set; }
        public bool UserCanRentBooks { get; set; }
    }
}
