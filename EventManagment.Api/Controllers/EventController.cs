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
        public EventController(ILogger<EventController> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }
        // GET: api/<EventController>
        [HttpGet]
        public async Task<IActionResult> GetAllEvents() 
            {
                var events = await _eventService.GetAll();
                return Ok(events);
            }

            // GET api/<EventController>/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var backedEvent= await _eventService.GetById(id);
            if (backedEvent is null)
                return BadRequest("event is null");
                return Ok(backedEvent);
            }

            // POST api/<EventController>
            [HttpPost]
            public async Task<IActionResult> CreateEvent([FromBody] AddEventDto dto)
            {
                var eventId = await _eventService.AddEvent(dto);
                return Ok(eventId);
            }

            // PUT api/<EventController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/<EventController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }
    }
}
