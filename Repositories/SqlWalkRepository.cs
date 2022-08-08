using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NZWalkContext context;

        public SqlWalkRepository(NZWalkContext context)
        {
            this.context = context;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await context.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid walkId)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(walk => walk.Id == walkId);

            if (existingWalk == null)
            {
                return null;
            }
            context.Walks.Remove(existingWalk);
            await context.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await context.Walks
                .Include(walk => walk.Region)
                .Include(walk => walk.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid walkId)
        {
            return await context.Walks
                .Include(walk => walk.Region)
                .Include(walk => walk.WalkDifficulty)
                .FirstOrDefaultAsync(walk => walk.Id == walkId);
        }

        public async Task<Walk> UpdateAsync(Guid walkId, Walk walk)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(walk => walk.Id == walkId);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Length = walk.Length;
            existingWalk.Name = walk.Name;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await context.SaveChangesAsync();
            return existingWalk;
        }
    }
}
