namespace Domain
{
    public class PackageTransportation
    {
        public int PackageId { get; set; }
        public int TransportationId { get; set; }
        public Package Package { get; set; }
        public Transportation Transportation { get; set; }
    }
}
