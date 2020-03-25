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
    public interface IServiceService
    {
        Task<int> AddServiceAsync(AddServiceDto addServiceDto);
        Task<int> DeleteServiceAsync(int id);
        Task<List<GetServiceDto>> FilterServicesAsync(FilterServiceDto filterServiceDto);
        Task<GetServiceDto> GetServiceAsync(int id);
        Task<List<GetServiceDto>> ListServiceAsync();
        Task<int> UpdateServiceAsync(int id, SetServiceDto setServiceDto);
    }
}
