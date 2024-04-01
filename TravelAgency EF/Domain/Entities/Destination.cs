using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DestinationId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public ICollection<Transportation> Transportations { get; set; }
        public ICollection<Accommodation> Accommodations { get; set; }
        public ICollection<PackageDestination> PackageDestination { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
