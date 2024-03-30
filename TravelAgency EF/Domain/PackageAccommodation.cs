namespace Domain
{
    public class PackageAccommodation
    {
        public int PackageId { get; set; }
        public int AccommodationId { get; set; }
        public Package Package { get; set; }
        public Accommodation Accommodation { get; set; }
    }
}
