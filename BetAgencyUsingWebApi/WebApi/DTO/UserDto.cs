using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
