namespace Domain.Entities
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegistrationDatTime { get; set; }
        public string Status { get; set; }
    }
}
