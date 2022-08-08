using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid walkId);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> DeleteAsync(Guid walkId);
        Task<Walk> UpdateAsync(Guid walkId, Walk walk);
    }
}
