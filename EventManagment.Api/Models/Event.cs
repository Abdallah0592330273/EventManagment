using System.Text.Json.Serialization;

namespace EventManagment.Api.Models;
public class Event
{
    public Guid EventId { get; private set; }
    public string EventName { get; private set; } = string.Empty;
    public string EventDescription { get; private set; } = string.Empty;
    public DateTime EventStartDate { get; private set; }
    public DateTime EventEndDate { get; private set; }
    public List<Tag>tags { get; private set; }
    /// <summary>
    /// Gets the user ID associated with the event.
    /// </summary>
    //public Guid UserId { get; private set; } = Guid.Empty;
    [JsonConstructor]
    private Event()
    {
        tags = new List<Tag>();
    }

    // 2. Your existing constructor
    private Event(string name, string description, DateTime startDate, DateTime endDate)
    {
        EventId = Guid.NewGuid();
        EventName = name;
        EventDescription = description;
        EventStartDate = startDate;
        EventEndDate = endDate;
        tags = new List<Tag>(); // Good practice to initialize lists!
    }
    public static Event Create(string name, string description, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("name should not be empty");
       return new Event(name, description, startDate, endDate);
    }

}

