namespace MyProject
{
    public class Reservation
    {
        public User MadeBy { get; set; }
        public bool CanBeCancelledBy(User user)
        {
            return user == MadeBy || user.isAdmin;
        }
    }

    public class User()
    {
        public bool isAdmin { get; set; }
    }
}
