using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters
{
    public class HotelCompanyConverter : IHotelCompanyConverter
    {
        public GetHotelCompanyDto HotelCompanyToGetHotelCompanyDto(HotelCompany hotelCompany)
        {
            return new GetHotelCompanyDto
            {
                Id = hotelCompany.Id,
                Name = hotelCompany.Name,
                Nip = hotelCompany.Nip,
                Street = hotelCompany.Street,
                BuildingNumber = hotelCompany.BuildingNumber,
                LocalNumber = hotelCompany.LocalNumber,
                PostalCode = hotelCompany.PostalCode,
                City = hotelCompany.City,
                PhoneNumber = hotelCompany.PhoneNumber
            };
        }

        public HotelCompany SetHotelCompanyToHotelCompany(SetHotelCompanyDto setHotelCompany)
        {
            return new HotelCompany()
            {
                Name = setHotelCompany.Name,
                Nip = setHotelCompany.Nip,
                Street = setHotelCompany.Street,
                BuildingNumber = setHotelCompany.BuildingNumber,
                LocalNumber = setHotelCompany.LocalNumber,
                PostalCode = setHotelCompany.PostalCode,
                City = setHotelCompany.City,
                PhoneNumber = setHotelCompany.PhoneNumber
            };
        }
    }
}
