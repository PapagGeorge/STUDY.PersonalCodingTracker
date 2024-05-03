using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTO_s
{
    public class RegistrationDTO
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime RegistrationDateTime { get; set; }
    }
}
