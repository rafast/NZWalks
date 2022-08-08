using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid walkDifficultyId);
        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteAsync(Guid walkDifficultyId);
        Task<WalkDifficulty> UpdateAsync(Guid walkDifficultyId, WalkDifficulty walkDifficulty);
    }
}
