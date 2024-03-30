using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PackageService
    {
        public long PackageId { get; set; }
        public long ServiceId { get; set; }
        public Package Package { get; set; }
        public Service Service { get; set; }
    }
}
