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
    public class ServiceService : IServiceService
    {
        private readonly IServiceConverter _converter;
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceConverter converter,
                                    IServiceRepository repository)
        {
            _converter = converter;
            _repository = repository;
        }
        public async Task<int> AddServiceAsync(AddServiceDto addServiceDto)
        {
            return await _repository.AddAsync(_converter.AddServiceDtoToService(addServiceDto));
        }

        public async Task<int> DeleteServiceAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<GetServiceDto>> FilterServicesAsync(FilterServiceDto filterServiceDto)
        {
            var filteredServices = await _repository.FilterAsync(filterServiceDto);
            return filteredServices.Select(s => _converter.ServiceToGetServiceDto(s)).ToList();
        }

        public async Task<GetServiceDto> GetServiceAsync(int id)
        {
            var service = await _repository.GetAsync(id);
            return _converter.ServiceToGetServiceDto(service);
        }

        public async Task<List<GetServiceDto>> ListServiceAsync()
        {
            var services = await _repository.ListAsync();
            return services.Select(s => _converter.ServiceToGetServiceDto(s)).ToList();
        }

        public async Task<int> UpdateServiceAsync(int id, SetServiceDto setServiceDto)
        {
            return await _repository.UpdateAsync(id, _converter.SetServiceDtoToService(setServiceDto));
        }
    }
}
