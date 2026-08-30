namespace EventManagment.Api.Models
{
    public class EventTagTracking
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid TagId { get; set; }
        public DateTime TagAddedTime { get; set; }
       // public Guid tagAddedBy { get; set; } = Guid.Empty;

    }
}
