using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PackageId { get; set; }
        public string PackageName { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public int Duration { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public ICollection<Destination> Destinations { get; set; }
        public ICollection<Accommodation> Accommodation { get; set; }
        public ICollection<Transportation> Transportation { get; set; }
        public ICollection<Service> Servicec { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
