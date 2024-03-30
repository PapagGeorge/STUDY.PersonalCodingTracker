namespace Domain
{
    public class PackageDestination
    {
        public int PackageId { get; set; }
        public int DestinationId { get; set; }
        public Package Package { get; set; }
        public Destination Destination { get; set; }
    }
}
