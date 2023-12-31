﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NZWalksAPi.Data;
using NZWalksAPi.Models.Domain;

namespace NZWalksAPi.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
                return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
           var existingWalk= await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int PageNo = 1, int PageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable(); 

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase)) {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks=isAscending? walks.OrderBy(x=>x.LengthInKm): walks.OrderByDescending(x=>x.LengthInKm);
                }
            }
            //Pagination
            var skipResults = (PageNo - 1) * PageSize;
            return await walks.Skip(skipResults).Take(PageSize).ToListAsync(); 
            //return await walks.ToListAsync();
           //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
          return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
