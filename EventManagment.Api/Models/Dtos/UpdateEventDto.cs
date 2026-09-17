using System.Data;

namespace EventManagment.Api.Models.Dtos
{
    public class UpdateEventDto
    {
       public Guid id { get; set; }
       public string name { get; set; } = string.Empty;
       public string? description { get; set; }
       public DateTime? startDate { get; set; }
       public DateTime? endDate { get; set; }
    }
}
