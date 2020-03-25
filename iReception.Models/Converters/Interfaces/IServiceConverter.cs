using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters.Interfaces
{
    public interface IServiceConverter
    {
        Service AddServiceDtoToService(AddServiceDto addServiceDto);
        GetServiceDto ServiceToGetServiceDto(Service service);
        Service SetServiceDtoToService(SetServiceDto setServiceDto);
    }
}
