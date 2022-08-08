using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SqlWalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalkContext context;

        public SqlWalkDifficultyRepository(NZWalkContext context)
        {
            this.context = context;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await context.AddAsync(walkDifficulty);
            await context.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid walkDifficultyId)
        {
            var existingWalkDifficulty = await context.WalkDifficulties.FirstOrDefaultAsync(walkDifficulty => walkDifficulty.Id == walkDifficultyId);

            if (existingWalkDifficulty == null)
            {
                return null;
            }
            context.WalkDifficulties.Remove(existingWalkDifficulty);
            await context.SaveChangesAsync();
            return existingWalkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await context.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid walkDifficultyId)
        {
            return await context.WalkDifficulties.FirstOrDefaultAsync(walkDifficulty => walkDifficulty.Id == walkDifficultyId);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid walkDifficultyId, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await context.WalkDifficulties.FirstOrDefaultAsync(walkDifficulty => walkDifficulty.Id == walkDifficultyId);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;

            await context.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
