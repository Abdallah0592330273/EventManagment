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
        public EventService(ApplicationDbContext context, ILogger<IEventService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Event>> GetAll(CancellationToken cancellationToken)
        {

            return await _context.Events.AsNoTracking()
                                        .Include(e => e.Tags)
                                        .ToListAsync(cancellationToken);
        }

        public async Task<Event?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Events.Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.EventId == id, cancellationToken);
        }
        public async Task<Guid> AddEvent(Event newEvent, CancellationToken cancellationToken)
        {
            if (newEvent == null)
                throw new ArgumentNullException(nameof(newEvent));

            _context.Events.Add(newEvent);
            
            await _context.SaveChangesAsync(cancellationToken); 
            return newEvent.EventId;
            //if ansync not found 
            //return Task.FromResult(newEvent.EventId);
        }

        public async Task<bool> DeleteEvent(Event e, CancellationToken cancellationToken)
        {
            if (e == null)
                return false;

            _context.Events.Remove(e);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task UpdateEvent(Guid id, Event uEvent,CancellationToken cancellationToken)
        {
            var existingEvent = _context.Events.Find(id);
            if (existingEvent == null) throw new ArgumentException($"Event with ID {id} not found.", nameof(id));
            if (uEvent.EventId == id)
            {
                existingEvent.Update(uEvent.EventName, uEvent.EventDescription, uEvent.EventStartDate, uEvent.EventEndDate);
            }
            _context.Events.Update(existingEvent);
            await _context.SaveChangesAsync(cancellationToken);
           
        }
        public async Task<List<Tag>> GetEventTags(Guid eventId, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.EventId == eventId, cancellationToken);
            if (eventEntity == null)
            {
                throw new ArgumentException($"Event with ID {eventId} not found.", nameof(eventId));
            }
            return eventEntity.Tags.ToList();
        }
        public async Task AddTagToEvent(Guid eventId, Tag tag, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.EventId == eventId, cancellationToken);
            if (eventEntity == null)
            {
                throw new ArgumentException($"Event with ID {eventId} not found.", nameof(eventId));
            }
            eventEntity.AddTag(tag);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task RemoveTagFromEvent(Guid eventId, Guid tagId, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.EventId == eventId, cancellationToken);
            if (eventEntity == null)
            {
                throw new ArgumentException($"Event with ID {eventId} not found.", nameof(eventId));
            }
            eventEntity.RemoveTag(tagId);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
