namespace EventManagment.Api.Models.Dtos
{
    public record AddEventDto(string EventName, string EventDescription,
                                DateTime EventStartDate, DateTime EventEndDate,List<TagDto>Tags
                               );
}
