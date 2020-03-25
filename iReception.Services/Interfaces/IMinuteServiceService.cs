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
    public interface IMinuteServiceService
    {
        Task<int> AddMinuteServiceAsync(AddMinuteServiceDto addMinuteServiceDto);
        Task<int> DeleteMinuteServiceAsync(int id);
        Task<List<GetMinuteServiceDto>> FilterMinuteServicesAsync(FilterMinuteServiceDto filterMinuteServiceDto);
        Task<GetMinuteServiceDto> GetMinuteServiceAsync(int id);
        Task<List<GetMinuteServiceDto>> ListMinuteServicesAsync();
        Task<int> UpdateMinuteServiceAsync(int id, SetMinuteServiceDto setMinuteServiceDto);
    }
}
