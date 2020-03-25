using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters
{
    public class ServiceConverter : IServiceConverter
    {
        public Service AddServiceDtoToService(AddServiceDto addServiceDto)
        {
            return new Service
            {
                Name = addServiceDto.Name,
                Description = addServiceDto.Description,
                IsAvailable = addServiceDto.IsAvailable
            };
        }

        public GetServiceDto ServiceToGetServiceDto(Service service)
        {
            return new GetServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                IsAvailable = service.IsAvailable
            };
        }

        public Service SetServiceDtoToService(SetServiceDto setServiceDto)
        {
            return new Service 
            {
                Name = setServiceDto.Name,
                Description = setServiceDto.Description,
                IsAvailable = setServiceDto.IsAvailable
            };

        }
    }
}
