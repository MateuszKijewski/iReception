using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> AddAsync(Service service);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Service>> FilterAsync(FilterServiceDto filterServiceDto);
        Task<Service> GetAsync(int id);
        Task<IEnumerable<Service>> ListAsync();
        Task<int> UpdateAsync(int id, Service service);
    }
}
