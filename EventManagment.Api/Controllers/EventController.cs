using EventManagment.Api.Models;
using EventManagment.Api.Models.Dtos;
using EventManagment.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;
        private readonly ITagService _tagService;
        public EventController(ILogger<EventController> logger, IEventService eventService,ITagService tagService)
        {
            _logger = logger;
            _eventService = eventService;
            _tagService= tagService;
        }
        // GET: api/<EventController>
        [HttpGet]
        public async Task<IActionResult> GetAllEvents() 
            {
                var events = await _eventService.GetAll();
            var dtos = events
         .Select(e => new EventDto(
             e.EventId,
             e.EventName,
             e.EventDescription,
             e.EventStartDate,
             e.EventEndDate,
             e.Tags
                 .Select(t => new TagDto(t.Name))
                 .ToList()
         ))
         .ToList();
            return Ok(dtos);
            }

            // GET api/<EventController>/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var backedEvent= await _eventService.GetById(id);
            if (backedEvent is null)
                return BadRequest("event is null");
            var dto = new EventDto(
                backedEvent.EventId,
                backedEvent.EventName,
                backedEvent.EventDescription,
                backedEvent.EventStartDate,
                backedEvent.EventEndDate,
                backedEvent.Tags
                    .Select(t => new TagDto(t.Name))
                    .ToList()
            );
            return Ok(dto);
        }

            // POST api/<EventController>
            [HttpPost]
            public async Task<IActionResult> CreateEvent([FromBody] AddEventDto dto,CancellationToken cancellationToken)
            {
            var tags = new List<Tag>();
            foreach(var tag in dto.Tags)
            {
                if(tag is not null)
                {
                   var existingTag=await _tagService.GetTagByName(tag.Name);
                    if (existingTag is null)
                        return BadRequest($"this{tag.Name} not exists in tags");
                    tags.Add(existingTag);
                    
                }

            }
            var eve = Event.Create(dto.EventName, dto.EventDescription, dto.EventStartDate, dto.EventEndDate,tags);
            var eventId = await _eventService.AddEvent(eve,cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = eventId }, dto);
            }

            // PUT api/<EventController>/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateEvent (Guid id, [FromBody] UpdateEventDto value)
            {
            var existingEvent = await _eventService.GetById(value.id);
            if(existingEvent is not null)
            {
                await _eventService.UpdateEvent(id, existingEvent);
                return NoContent();
            }
            return BadRequest();

            }

            // DELETE api/<EventController>/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var existingEvent = await _eventService.GetById(id);
                if (existingEvent is null)
                    return BadRequest("Event not found");

                var result =_eventService.DeleteEvent(existingEvent);
                return Ok(result);
            }
    }
}
