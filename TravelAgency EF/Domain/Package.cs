using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Package
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public ICollection<Destination> Destinations { get; set; }
        public ICollection<Accommodation> Accommodation { get; set; }
        public ICollection<Transportation> Transportation { get; set; }
        public ICollection<Service> Servicec { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
