namespace Domain.Dtos
{
    public class UserReadDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
    }
}
