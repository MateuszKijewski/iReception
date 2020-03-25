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
    public class ServiceRepository : IServiceRepository
    {
        private readonly iReceptionDbContext _db;

        public ServiceRepository(iReceptionDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Service service)
        {
            service.IsDeleted = false;
            await _db.Services.AddAsync(service);
            return service.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var serviceToDelete = await _db.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (serviceToDelete != null)
            {
                if (!serviceToDelete.IsDeleted)
                {
                    serviceToDelete.IsDeleted = true;
                }
                throw new KeyNotFoundException("Requested service is deleted");
            }
            throw new NullReferenceException("Requested service does not exist");

        }

        public async Task<IEnumerable<Service>> FilterAsync(FilterServiceDto filterServiceDto)
        {
            int filters = 0;
            bool availableFiltered = false;

            List<Service> result = new List<Service>();

            int minId = filterServiceDto.MinId ?? 0;
            int maxId = filterServiceDto.MaxId ?? 0;

            if (minId > 0 || maxId > 0)
            {
                var filteredMinIds = await _db.Services.Where(s => s.Id >= minId).ToListAsync();
                var filteredMaxIds = await _db.Services.Where(s => s.Id <= maxId).ToListAsync();

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

            if (!string.IsNullOrEmpty(filterServiceDto.Name))
            {
                var filteredNames = await _db.Services.Where(s => EF.Functions.Like(s.Name, $"%{filterServiceDto.Name}%")).ToListAsync();
                result.AddRange(filteredNames);
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterServiceDto.Description))
            {
                var filteredDescriptions = await _db.Services.Where(s => EF.Functions.Like(s.Description, $"%{filterServiceDto.Description}%")).ToListAsync();
                result.AddRange(filteredDescriptions);
                filters += 1;
            }

            if (filterServiceDto.IsAvailable)
            {
                var filteredAvailableServices = await _db.Services.Where(s => s.IsAvailable).ToListAsync();
                result.AddRange(filteredAvailableServices);
                if (!availableFiltered) { availableFiltered = true; }
            }
            if (filterServiceDto.IsNotAvailable)
            {
                var filteredNotAvailableServices = await _db.Services.Where(s => !s.IsAvailable).ToListAsync();
                result.AddRange(filteredNotAvailableServices);
                if (!availableFiltered) { availableFiltered = true; }
            }

            if (availableFiltered) { filters += 1; }
            var groupedResults = result.GroupBy(r => r);
            List<Service> finalResult = new List<Service>();
            foreach (var gr in groupedResults)
            {
                if (gr.Count() == filters) { finalResult.Add(gr.Key); }
            }

            return finalResult;
        }

        public async Task<Service> GetAsync(int id)
        {
            var serviceToGet = await _db.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (serviceToGet != null)
            {
                if (!serviceToGet.IsDeleted)
                {
                    return serviceToGet;
                }
                throw new KeyNotFoundException("Requested service is deleted");
            }
            throw new NullReferenceException("Requested service does not exist");
        }

        public async Task<IEnumerable<Service>> ListAsync()
        {
            var services = await _db.Services.ToListAsync();
            return services.Where(s => !s.IsDeleted);
        }

        public async Task<int> UpdateAsync(int id, Service service)
        {
            var serviceToUpdate = await _db.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (serviceToUpdate != null)
            {
                if (!serviceToUpdate.IsDeleted)
                {
                    foreach (PropertyInfo prop in service.GetType().GetProperties())
                    {
                        var value = prop.GetValue(service);
                        if (value != null && prop.Name.ToLower() != "id")
                        {
                            prop.SetValue(serviceToUpdate, value);
                        }
                    }
                    _db.Services.Update(serviceToUpdate);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Requested service is deleted");
            }
            throw new NullReferenceException("Requested service does not exist");
        }
    }
}
