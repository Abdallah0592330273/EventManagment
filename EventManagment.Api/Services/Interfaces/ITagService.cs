using EventManagment.Api.Models;

namespace EventManagment.Api.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetTags();
        Task<Tag?> GetTagByName(string name);
        Task<Guid> CreateTag(Tag tag);
        Task UpdateTag(Guid tagId, Tag tag);
        Task DeleteTag(Guid tagId);
    }
}
