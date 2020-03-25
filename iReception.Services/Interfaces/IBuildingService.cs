using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Services.Interfaces
{
    public interface IBuildingService
    {
        Task<int> AddBuildingAsync(AddBuildingDto addBuildingDto);
        Task<GetBuildingDto> GetBuildingAsync(int id);
        Task<List<GetBuildingDto>> ListBuildingsAsync();
        Task<int> UpdateBuildingAsync(int id, SetBuildingDto setBuildingDto);
        Task<int> DeleteBuildingAsync(int id);
        Task<List<GetBuildingDto>> FilterBuildingsAsync(FilterBuildingDto filterBuildingDto);
    }
}
