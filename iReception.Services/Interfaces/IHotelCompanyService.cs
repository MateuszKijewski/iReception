using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;

namespace iReception.Services.Interfaces
{
    public interface IHotelCompanyService
    {
        Task<int> AddHotelCompanyAsync();
        Task<GetHotelCompanyDto> GetHotelCompanyAsync();
        Task<int> UpdateHotelCompanyAsync(SetHotelCompanyDto setHotelCompanyDto);
    }
}
