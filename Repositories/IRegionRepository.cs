using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid regionId);
        Task<Region> AddAsync(Region region);
        Task<Region> DeleteAsync(Guid regionId);
        Task<Region> UpdateAsync(Guid regionId, Region region);
    }
}
