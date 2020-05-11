using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters.Interfaces
{
    public interface IMinuteServiceToReservationConverter
    {
        MinuteServiceToReservation AddMinuteServiceToReservationDtoToMinuteServiceToReservation(
            AddMinuteServiceToReservationDto addMinuteServiceToReservationDto);

        GetMinuteServiceToReservationDto MinuteServiceToReservationToGetMinuteServiceToReservationDto(
            MinuteServiceToReservation minuteServiceToReservation);
    }
}
