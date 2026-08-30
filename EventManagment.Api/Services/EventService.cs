using EventManagment.Api.Data;
using EventManagment.Api.Models;
using EventManagment.Api.Models.Dtos;
using EventManagment.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EventManagment.Api.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IEventService> _logger;
        public EventService(ApplicationDbContext context,ILogger<IEventService>logger)
        {
            _context = context;
            _logger = logger;
        }
        public Task<Guid> AddEvent(AddEventDto newEvent)
        { 
            var existingEvent = _context.Events.FirstOrDefault(e => e.EventName == newEvent.EventName);
            if (existingEvent != null)
            {
                _logger.LogError("this was found before");
                throw new InvalidOperationException($"An event with name {newEvent.EventName} already exists.");
            }
            var eventToAdd = Event.Create(newEvent.EventName, newEvent.EventDescription, newEvent.EventStartDate, newEvent.EventEndDate);
            _context.Events.Add(eventToAdd);
            _context.SaveChanges();
            return Task.FromResult(eventToAdd.EventId);
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetById(Guid id)
        {
           var existingEvent= await _context.Events.FirstOrDefaultAsync(e => e.EventId == id);
            if (existingEvent is null)
            {
                return null;
                _logger.LogWarning("event null refrence now ");
            }
            return existingEvent;
        }
        public bool DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }


        public void UpdateEvent(int id, Event updatedEventDto)
        {
            throw new NotImplementedException();
        }
    }
}
