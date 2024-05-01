namespace Domain.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int OrganizerId { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool isDeleted { get; set; }
    }
}
