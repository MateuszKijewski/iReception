using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.SetDtos;
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
    public class MinuteServiceRepository : IMinuteServiceRepository
    {
        private readonly iReceptionDbContext _db;

        public MinuteServiceRepository(iReceptionDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(MinuteService minuteService)
        {
            minuteService.IsDeleted = false;
            await _db.MinuteServices.AddAsync(minuteService);
            return minuteService.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var minuteServiceToDelete = await _db.MinuteServices.FirstOrDefaultAsync(ms => ms.Id == id);
            if (minuteServiceToDelete != null)
            {
                if (!minuteServiceToDelete.IsDeleted)
                {
                    minuteServiceToDelete.IsDeleted = true;
                    _db.MinuteServices.Update(minuteServiceToDelete);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Requested minute service is deleted");
            }
            throw new NullReferenceException("Requested minute service does not exist");
        }

        public async Task<IEnumerable<MinuteService>> FilterAsync(FilterMinuteServiceDto filterMinuteServiceDto)
        {
            int filters = 0;
            bool availableFiltered = false;

            List<MinuteService> result = new List<MinuteService>();

            int minId = filterMinuteServiceDto.MinId ?? 0;
            int maxId = filterMinuteServiceDto.MaxId ?? 0;

            if (minId > 0 || maxId > 0)
            {
                var filteredMinIds = await _db.MinuteServices.Where(ms => ms.Id >= minId).ToListAsync();
                var filteredMaxIds = await _db.MinuteServices.Where(ms => ms.Id <= maxId).ToListAsync();

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

            if (!string.IsNullOrEmpty(filterMinuteServiceDto.Name))
            {
                var filteredNames = await _db.MinuteServices.Where(ms => EF.Functions.Like(ms.Name, $"%{filterMinuteServiceDto.Name}%")).ToListAsync();
                result.AddRange(filteredNames);
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterMinuteServiceDto.Description))
            {
                var filteredDescriptions = await _db.MinuteServices.Where(ms => EF.Functions.Like(ms.Name, $"%{filterMinuteServiceDto.Description}%")).ToListAsync();
                result.AddRange(filteredDescriptions);
                filters += 1;
            }

            decimal minPricePerMinute = filterMinuteServiceDto.MinPricePerMinute ?? 0;
            decimal maxPricePerMinute = filterMinuteServiceDto.MaxPricePerMinute ?? 0;
            if ( minPricePerMinute > 0 || maxPricePerMinute > 0)
            {
                var filteredMinPricesPerMinute = await _db.MinuteServices.Where(ms => ms.PricePerMinute >= minPricePerMinute).ToListAsync();
                var filteredMaxPricesPerMinute = await _db.MinuteServices.Where(ms => ms.PricePerMinute <= maxPricePerMinute).ToListAsync();

                if (minPricePerMinute > 0 && maxPricePerMinute == 0)
                {
                    result.AddRange(filteredMinPricesPerMinute);
                }
                else if (minPricePerMinute > 0 && maxPricePerMinute == 0)
                {
                    result.AddRange(filteredMaxPricesPerMinute);
                }

                result.AddRange(filteredMaxPricesPerMinute.Intersect(filteredMinPricesPerMinute));
                filters += 1;
            }

            int minTime = filterMinuteServiceDto.MinTime ?? 0;
            if (minTime != 0)
            {
                var filteredMinTimes = await _db.MinuteServices.Where(ms => ms.MinTime <= filterMinuteServiceDto.MinTime).ToListAsync();
                result.AddRange(filteredMinTimes);
                filters += 1;
            }

            int maxTime = filterMinuteServiceDto.MaxTime ?? 0;
            if (maxTime != 0)
            {
                var filteredMaxTimes = await _db.MinuteServices.Where(ms => ms.MaxTime >= filterMinuteServiceDto.MaxTime).ToListAsync();
                result.AddRange(filteredMaxTimes);
                filters += 1;
            }

            if (filterMinuteServiceDto.IsAvailable)
            {
                var availableMinuteServices = await _db.MinuteServices.Where(ms => ms.IsAvailable).ToListAsync();
                result.AddRange(availableMinuteServices);
                if (!availableFiltered) { availableFiltered = true; }
            }
            if (filterMinuteServiceDto.IsNotAvailable)
            {
                var filteredNotAvailableMinuteServices = await _db.MinuteServices.Where(ms => !ms.IsAvailable).ToListAsync();
                result.AddRange(filteredNotAvailableMinuteServices);
                if (!availableFiltered) { availableFiltered = true; }
            }
            
            if (availableFiltered) { filters += 1; }
            var groupedResults = result.GroupBy(r => r);
            List<MinuteService> finalResult = new List<MinuteService>();
            foreach(var gr in groupedResults)
            {
                if (gr.Count() == filters) { finalResult.Add(gr.Key); }
            }

            return finalResult;
        }

        public async Task<MinuteService> GetAsync(int id)
        {
            var minuteServiceToGet = await _db.MinuteServices.FirstOrDefaultAsync(ms => ms.Id == id);
            if (minuteServiceToGet != null)
            {
                if (!minuteServiceToGet.IsDeleted)
                {
                    return minuteServiceToGet;
                }
                throw new KeyNotFoundException("Requested minute service is deleted");
            }
            throw new NullReferenceException("Requested minute service does not exist");
        }

        public async Task<IEnumerable<MinuteService>> ListAsync()
        {
            var minuteServices = await _db.MinuteServices.ToListAsync();
            return minuteServices.Where(ms => !ms.IsDeleted);
        }

        public async Task<int> UpdateAsync(int id, MinuteService minuteService)
        {
            var minuteServiceToUpdate = await _db.MinuteServices.FirstOrDefaultAsync(ms => ms.Id == id);
            if (minuteServiceToUpdate != null)
            {
                if (!minuteServiceToUpdate.IsDeleted)
                {
                    foreach (PropertyInfo prop in minuteService.GetType().GetProperties())
                    {
                        var value = prop.GetValue(minuteService);
                        if (value != null && prop.Name.ToLower() != "id")
                        {
                            prop.SetValue(minuteServiceToUpdate, value);
                        }
                    }
                    _db.MinuteServices.Update(minuteServiceToUpdate);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Requested minute service is deleted");
            }
            throw new NullReferenceException("Requested minute service does not exist");
        }
    }
}
