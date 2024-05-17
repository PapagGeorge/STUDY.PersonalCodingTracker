using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTO_s
{
    public class EventDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int OrganizerId { get; set; }
        [Required]
        public int Capacity { get; set; }
        
    }
}
