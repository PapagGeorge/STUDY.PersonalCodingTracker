using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTO_s
{
    public class AttendanceDTO
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
