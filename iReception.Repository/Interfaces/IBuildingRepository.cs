using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IBuildingRepository
    {
        Task<int> AddAsync(Building building);
        Task<Building> GetAsync(int id);
        Task<IEnumerable<Building>> ListAsync();
        Task<int> UpdateAsync(int id, Building building);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Building>> FilterAsync(FilterBuildingDto filterBuildingDto);
    }
}
