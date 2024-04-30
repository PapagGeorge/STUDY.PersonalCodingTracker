namespace Domain.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime AttendanceDateTime { get; set; }
    }
}
