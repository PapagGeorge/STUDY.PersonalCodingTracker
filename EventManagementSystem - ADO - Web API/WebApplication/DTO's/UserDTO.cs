using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTO_s
{
    public class UserDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobilePhone { get; set; }
    }
}
