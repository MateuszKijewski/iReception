using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Entities;

namespace iReception.Repository.Interfaces
{
    public interface IHotelCompanyRepository
    {
        Task<int> AddAsync();
        Task<HotelCompany> GetAsync();
        Task<int> UpdateAsync(HotelCompany hotelCompany);
    }
}
