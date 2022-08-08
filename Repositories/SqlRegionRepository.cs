using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalkContext context;

        public SqlRegionRepository(NZWalkContext context)
        {
            this.context = context;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await context.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid regionId)
        {
            var region = await context.Regions.FirstOrDefaultAsync(region => region.Id == regionId);

            if (region == null)
            {
                return null;
            }
            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid regionId)
        {
            return await context.Regions.FirstOrDefaultAsync(region => region.Id == regionId);
        }

        public async Task<Region> UpdateAsync(Guid regionId, Region region)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(region => region.Id == regionId);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Lng = region.Lng;
            existingRegion.Population = region.Population;

            await context.SaveChangesAsync();
            return existingRegion;
        }
    }
}
