namespace Domain
{
    public class PackageService
    {
        public int PackageId { get; set; }
        public int ServiceId { get; set; }
        public Package Package { get; set; }
        public Service Service { get; set; }
    }
}
