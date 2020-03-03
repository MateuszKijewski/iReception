using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly iReceptionDbContext _db;

        public BuildingRepository(iReceptionDbContext db)
        {
            _db = db;
        }


        public async Task<int> AddAsync(Building building)
        {            
            building.IsAvailable = true;
            building.IsDeleted = false;
            await _db.Buildings.AddAsync(building);
            await _db.SaveChangesAsync();
            return building.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var buildingToDelete = await _db.Buildings.FirstOrDefaultAsync(b => b.Id == id);
            if(buildingToDelete != null)
            {
                if (!buildingToDelete.IsDeleted)
                {
                    buildingToDelete.IsDeleted = true;
                    _db.Buildings.Update(buildingToDelete);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Building is already deleted");
            }
            throw new NullReferenceException("Requested building does not exist");
        }

        public async Task<IEnumerable<Building>> FilterAsync(FilterBuildingDto filterBuildingDto)
        {
            int minId = filterBuildingDto.MinId ?? 0;
            int maxId = filterBuildingDto.MaxId ?? 0;
            int filters = 0;
            bool availabilityFiltered = false;

            List<Building> result = new List<Building>();
            if (minId > 0 || maxId > 0)
            {
                var filteredMinIds = await _db.Buildings.Where(b => b.Id >= minId).ToListAsync();
                var filteredMaxIds = await _db.Buildings.Where(b => b.Id <= maxId).ToListAsync();

                if (minId > 0 && maxId == 0)
                {
                    result.AddRange(filteredMinIds);
                }
                else if (maxId > 0 && minId == 0)
                {
                    result.AddRange(filteredMaxIds);
                }

                result.AddRange(filteredMaxIds.Intersect(filteredMinIds));
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterBuildingDto.Address))
            {
                var filteredAddresses = await _db.Buildings.Where(b => EF.Functions.Like(b.Address, $"%{filterBuildingDto.Address}%")).ToListAsync();
                result.AddRange(filteredAddresses);
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterBuildingDto.ShortName))
            {
                var filteredShortNames = await _db.Buildings.Where(b => EF.Functions.Like(b.ShortName, $"%{filterBuildingDto.ShortName}%")).ToListAsync();
                result.AddRange(filteredShortNames);
                filters += 1;
            }

            if (filterBuildingDto.IsAvailable)
            {
                var availableBuldings = await _db.Buildings.Where(b => b.IsAvailable).ToListAsync();
                result.AddRange(availableBuldings);
                if (!availabilityFiltered) { availabilityFiltered = true; }
            }
            if (filterBuildingDto.IsNotAvailable)
            {
                var notAvailableBuildings = await _db.Buildings.Where(b => !b.IsAvailable).ToListAsync();
                result.AddRange(notAvailableBuildings);
                if (!availabilityFiltered) { availabilityFiltered = true; }

            }

            if (availabilityFiltered) { filters += 1; }
            var groupedResults = result.GroupBy(r => r);
            List<Building> finalResult = new List<Building>();
            foreach(var gr in groupedResults)
            {
                if (gr.Count() == filters) { finalResult.Add(gr.Key); }
            }


            return finalResult;
        }

        public async Task<Building> GetAsync(int id)
        {
            var buildingGet = await _db.Buildings.FirstOrDefaultAsync(b => b.Id == id);
            if (buildingGet != null)
            {
                if (!buildingGet.IsDeleted)
                {
                    return buildingGet;
                }
                throw new KeyNotFoundException("Requested building is deleted");
            }
            throw new NullReferenceException("Building not found in database");
        }

        public async Task<IEnumerable<Building>> ListAsync()
        {
            var buildings = await _db.Buildings.ToListAsync();
            return buildings.Where(b => !b.IsDeleted);
        }

        public async Task<int> UpdateAsync(int id, Building building)
        {
            var buildingToUpdate = await _db.Buildings.FirstOrDefaultAsync(b => b.Id == id);
            if (buildingToUpdate != null)
            {
                if (!buildingToUpdate.IsDeleted)
                {
                    foreach (PropertyInfo prop in building.GetType().GetProperties())
                    {
                        var value = prop.GetValue(building);
                        if (value != null && prop.Name.ToLower() != "id")
                        {
                            prop.SetValue(buildingToUpdate, value);
                        }
                    }
                    _db.Buildings.Update(buildingToUpdate);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Requested building is deleted");
            }
            throw new NullReferenceException("Requested building does not exist");
        }
    }
}
