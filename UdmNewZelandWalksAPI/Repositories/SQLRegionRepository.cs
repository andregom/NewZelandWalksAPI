using Microsoft.EntityFrameworkCore;

using UdmNewZelandWalksAPI.Database;
using UdmNewZelandWalksAPI.Models.DTO;
using UdmNewZelandWalksAPI.Models.Domain;

namespace UdmNewZelandWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var dBQueryRegion = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            if (dBQueryRegion == null)
            {
                return null;
            }

            dBQueryRegion.Code = region.Code;
            dBQueryRegion.Name = region.Name;
            dBQueryRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return dBQueryRegion;
        }
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
