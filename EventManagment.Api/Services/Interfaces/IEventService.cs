using EventManagment.Api.Models;
using EventManagment.Api.Models.Dtos;

namespace EventManagment.Api.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAll(CancellationToken cancellationToken=default);
      //  Task<IEnumerable<Tag>> GetEventTags(int userId);
        Task<Event?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddEvent(Event newEvent, CancellationToken cancellationToken = default);
        Task UpdateEvent(Guid id, Event uEvent,CancellationToken cancellationToken = default);
        Task<bool> DeleteEvent(Event e,CancellationToken cancellationToken = default);
        
    }
}
