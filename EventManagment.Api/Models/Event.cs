namespace EventManagment.Api.Models;

public sealed class Event
{
    private Event()
    {
        // Required by EF Core when materializing an Event from the database.
    }

    private Event(
        string name,
        string description,
        DateTime startDate,
        DateTime endDate,
        List<Tag>? tags)
    {
        EventId = Guid.NewGuid();
        EventName = name;
        EventDescription = description;
        EventStartDate = startDate;
        EventEndDate = endDate;
        Tags = tags ?? new List<Tag>();
    }

    public Guid EventId { get; private set; }

    public string EventName { get; private set; } = string.Empty;

    public string EventDescription { get; private set; } = string.Empty;

    public DateTime EventStartDate { get; private set; }

    public DateTime EventEndDate { get; private set; }
    //Navigation property for the many-to-many relationship with Tag

    public List<Tag> Tags { get; private set; }
       

    public static Event Create(
        string name,
        string description,
        DateTime startDate,
        DateTime endDate,
        List<Tag>? tags)
    {
        ValidateEvent(name, startDate, endDate);

        return new Event(name, description, startDate, endDate, tags);
    }

    public void Update(
        string name,
        string description,
        DateTime? startDate,
        DateTime? endDate)
    {
        DateTime updatedStartDate = startDate ?? EventStartDate;
        DateTime updatedEndDate = endDate ?? EventEndDate;

        Validate(name, updatedStartDate, updatedEndDate);

        EventName = name.Trim();
        EventDescription = description?.Trim() ?? string.Empty;
        EventStartDate = updatedStartDate;
        EventEndDate = updatedEndDate;
    }

    public void AddTag(Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        bool tagAlreadyAdded = Tags.Any(
            t => t.Id == tag.Id);

        if (tagAlreadyAdded)
        {
            return;
        }

        Tags.Add(tag);
    }

    public void RemoveTag(Guid tagId)
    {
        Tag? tag = Tags.FirstOrDefault(
            t => t.Id == tagId);

        if (tag is not null)
        {
            Tags.Remove(tag);
        }
    }

    private static void Validate(
        string name,
        DateTime startDate,
        DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Event name cannot be empty.",
                nameof(name));
        }

        if (endDate <= startDate)
        {
            throw new ArgumentException(
                "Event end date must be after its start date.",
                nameof(endDate));
        }

        return;
    }

    private static void ValidateEvent(
        string name,
        DateTime startDate,
        DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Event name cannot be empty.",
                nameof(name));
        }

        if (endDate <= startDate)
        {
            throw new ArgumentException(
                "Event end date must be after its start date.",
                nameof(endDate));
        }
    }
}
