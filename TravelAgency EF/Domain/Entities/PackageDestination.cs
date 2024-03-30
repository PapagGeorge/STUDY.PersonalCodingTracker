using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PackageDestination
    {
        
        public long PackageId { get; set; }
        public long DestinationId { get; set; }
        public Package Package { get; set; }
        public Destination Destination { get; set; }
    }
}
