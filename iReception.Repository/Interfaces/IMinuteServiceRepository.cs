using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IMinuteServiceRepository
    {
        Task<int> AddAsync(MinuteService minuteService);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<MinuteService>> FilterAsync(FilterMinuteServiceDto filterMinuteServiceDto);
        Task<MinuteService> GetAsync(int id);
        Task<IEnumerable<MinuteService>> ListAsync();
        Task<int> UpdateAsync(int id, MinuteService minuteService);
    }
}
