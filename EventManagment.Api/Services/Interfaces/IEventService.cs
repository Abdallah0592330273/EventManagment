using EventManagment.Api.Models;
using EventManagment.Api.Models.Dtos;

namespace EventManagment.Api.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAll();
      //  Task<IEnumerable<Tag>> GetEventTags(int userId);
        Task<Event?> GetById(Guid id);
        Task<Guid> AddEvent(AddEventDto newEvent);
        void UpdateEvent(int id ,Event updatedEvent);
        bool DeleteEvent(int id);
        
    }
}
