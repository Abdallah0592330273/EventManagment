using EventManagment.Api.Models;
using EventManagment.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            return Ok(await _tagService.GetTags());

        }
        [HttpGet("string:{name}")]
        public async Task<IActionResult> GetTagByName(string name)
        {
            var tag = await _tagService.GetTagByName(name);
            if (tag == null) 
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tag tag, CancellationToken ct)
        {

            var TagId = await _tagService.CreateTag(tag);
            return Ok(TagId);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTag([FromRoute] Guid id, [FromBody] Tag tag)
        {
            await _tagService.UpdateTag(id, tag);
            return NoContent();

        }
        [HttpDelete]
        public async Task<IActionResult>RemoveTag(Guid id){
            if (id == Guid.Empty) return BadRequest("should't id be null brother");
            await _tagService.DeleteTag(id);
            return NoContent();
        }

    }

}
