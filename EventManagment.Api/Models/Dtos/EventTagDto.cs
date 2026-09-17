namespace EventManagment.Api.Models.Dtos
{
    public class EventTagDto
    {
        public Guid TagId { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Purpose { get; init; } = string.Empty;

        public DateTime TagAddedTime { get; init; }
    }
}
