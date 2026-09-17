using EventManagment.Api.Data;
using EventManagment.Api.Models;
using EventManagment.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManagment.Api.Services
{
    public class TagService : ITagService
    
{
        private readonly ApplicationDbContext _context;
        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return await Task.FromResult(tag.Id);
        }

        public async Task DeleteTag(Guid tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Tag?> GetTagByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);

            }
            return null;
                
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _context.Tags.AsNoTracking()
                                      .ToListAsync();
        }

        public async Task UpdateTag(Guid TagId, Tag tag)
        {
            var existingTag = await _context.Tags.FindAsync(TagId);
            if (existingTag == null)
            {
                throw new ArgumentException($"Tag with ID {TagId} not found.", nameof(TagId));
            }
            existingTag.Update(tag.Name, tag.Description);
            _context.Tags.Update(existingTag);
            await _context.SaveChangesAsync();  
        }
    }
}
