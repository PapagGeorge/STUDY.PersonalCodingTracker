using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PackageTransportation
    {
        public long PackageId { get; set; }
        public long TransportationId { get; set; }
        public Package Package { get; set; }
        public Transportation Transportation { get; set; }
    }
}
