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
    public class MinuteServiceConverter : IMinuteServiceConverter
    {
        public MinuteService AddMinuteServiceDtoToMinuteService(AddMinuteServiceDto addMinuteServiceDto)
        {
            return new MinuteService
            {
                Name = addMinuteServiceDto.Name,
                Description = addMinuteServiceDto.Description,
                PricePerMinute = addMinuteServiceDto.PricePerMinute,
                MinTime = addMinuteServiceDto.MinTime,
                MaxTime = addMinuteServiceDto.MaxTime,
                IsAvailable = addMinuteServiceDto.IsAvailable
            };
        }

        public GetMinuteServiceDto MinuteServiceToGetMinuteServiceDto(MinuteService minuteService)
        {
            return new GetMinuteServiceDto
            {
                Id = minuteService.Id,
                Name = minuteService.Name,
                Description = minuteService.Description,
                PricePerMinute = minuteService.PricePerMinute,
                MinTime = minuteService.MinTime,
                MaxTime = minuteService.MaxTime,
                IsAvailable = minuteService.IsAvailable
            };
        }

        public MinuteService SetMinuteServiceDtoToMinuteService(SetMinuteServiceDto setMinuteServiceDto)
        {
            return new MinuteService
            {
                Name = setMinuteServiceDto.Name,
                Description = setMinuteServiceDto.Description,
                PricePerMinute = setMinuteServiceDto.PricePerMinute,
                MinTime = setMinuteServiceDto.MinTime,
                MaxTime = setMinuteServiceDto.MaxTime,
                IsAvailable = setMinuteServiceDto.IsAvailable
            };
        }
    }
}
