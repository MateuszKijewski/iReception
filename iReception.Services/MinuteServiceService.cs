using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Repository.Interfaces;
using iReception.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Services
{
    public class MinuteServiceService : IMinuteServiceService
    {
        private readonly IMinuteServiceConverter _converter;
        private readonly IMinuteServiceRepository _repository;

        public MinuteServiceService(IMinuteServiceConverter converter,
                                    IMinuteServiceRepository repository)
        {
            _converter = converter;
            _repository = repository;
        }

        public async Task<int> AddMinuteServiceAsync(AddMinuteServiceDto addMinuteServiceDto)
        {
            return await _repository.AddAsync(_converter.AddMinuteServiceDtoToMinuteService(addMinuteServiceDto));
        }

        public async Task<int> DeleteMinuteServiceAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<GetMinuteServiceDto>> FilterMinuteServicesAsync(FilterMinuteServiceDto filterMinuteServiceDto)
        {
            var filteredMinuteServices = await _repository.FilterAsync(filterMinuteServiceDto);
            return filteredMinuteServices.Select(ms => _converter.MinuteServiceToGetMinuteServiceDto(ms)).ToList();
        }

        public async Task<GetMinuteServiceDto> GetMinuteServiceAsync(int id)
        {
            var minuteService = await _repository.GetAsync(id);
            return _converter.MinuteServiceToGetMinuteServiceDto(minuteService);
        }

        public async Task<List<GetMinuteServiceDto>> ListMinuteServicesAsync()
        {
            var minuteServices = await _repository.ListAsync();
            return minuteServices.Select(ms => _converter.MinuteServiceToGetMinuteServiceDto(ms)).ToList();
        }

        public async Task<int> UpdateMinuteServiceAsync(int id, SetMinuteServiceDto setMinuteServiceDto)
        {
            return await _repository.UpdateAsync(id, _converter.SetMinuteServiceDtoToMinuteService(setMinuteServiceDto));
        }
    }
}
