namespace EventManagment.Api.Models.Dtos
{
    public record EventDto(Guid id,string EventName, string EventDescription,
                                    DateTime EventStartDate, DateTime EventEndDate);
}
