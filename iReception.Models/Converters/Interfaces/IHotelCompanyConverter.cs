using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters.Interfaces
{
    public interface IHotelCompanyConverter
    {
        GetHotelCompanyDto HotelCompanyToGetHotelCompanyDto(HotelCompany hotelCompany);
        HotelCompany SetHotelCompanyToHotelCompany(SetHotelCompanyDto setHotelCompany);
    }
}
