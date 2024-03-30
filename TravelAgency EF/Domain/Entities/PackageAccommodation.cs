using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PackageAccommodation
    {
        
        public long PackageId { get; set; }
        public long AccommodationId { get; set; }
        public Package Package { get; set; }
        public Accommodation Accommodation { get; set; }
    }
}
