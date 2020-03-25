using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters.Interfaces
{
    public interface IMinuteServiceConverter
    {
        MinuteService AddMinuteServiceDtoToMinuteService(AddMinuteServiceDto addMinuteServiceDto);
        GetMinuteServiceDto MinuteServiceToGetMinuteServiceDto(MinuteService minuteService);
        MinuteService SetMinuteServiceDtoToMinuteService(SetMinuteServiceDto setMinuteServiceDto);
    }
}
