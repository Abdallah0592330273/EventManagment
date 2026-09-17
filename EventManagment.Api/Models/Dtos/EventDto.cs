namespace EventManagment.Api.Models.Dtos
{
    public record EventDto(Guid id,string Name, string Description,
                                    DateTime StartDate, DateTime EndDate
                                    , List<TagDto> Tags);
}
