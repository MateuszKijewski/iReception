using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Repository.Interfaces;
using iReception.Services.Interfaces;

namespace iReception.Services
{
    public class HotelCompanyService : IHotelCompanyService
    {
        private readonly IHotelCompanyRepository _hotelCompanyRepository;
        private readonly IHotelCompanyConverter _hotelCompanyConverter;

        public HotelCompanyService(IHotelCompanyRepository hotelCompanyRepository,
                                    IHotelCompanyConverter hotelCompanyConverter)
        {
            _hotelCompanyRepository = hotelCompanyRepository;
            _hotelCompanyConverter = hotelCompanyConverter;
        }

        public async Task<int> AddHotelCompanyAsync()
        {
            var createdHotelCompanyId = await _hotelCompanyRepository.AddAsync();

            return createdHotelCompanyId;
        }

        public async Task<GetHotelCompanyDto> GetHotelCompanyAsync()
        {
            var hotelCompany = await _hotelCompanyRepository.GetAsync();
            var getHotelCompanyDto = _hotelCompanyConverter.HotelCompanyToGetHotelCompanyDto(hotelCompany);

            return getHotelCompanyDto;
        }

        public async Task<int> UpdateHotelCompanyAsync(SetHotelCompanyDto setHotelCompanyDto)
        {
            var hotelCompany = _hotelCompanyConverter.SetHotelCompanyToHotelCompany(setHotelCompanyDto);
            var updatedHotelCompanyId = await _hotelCompanyRepository.UpdateAsync(hotelCompany);

            return updatedHotelCompanyId;
        }
    }
}
