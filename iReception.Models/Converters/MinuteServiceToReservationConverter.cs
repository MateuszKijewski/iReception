using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters
{
    public class MinuteServiceToReservationConverter : IMinuteServiceToReservationConverter
    {
        public MinuteServiceToReservation AddMinuteServiceToReservationDtoToMinuteServiceToReservation(
            AddMinuteServiceToReservationDto addMinuteServiceToReservationDto)
        {
            return new MinuteServiceToReservation()
            {
                ReservationId = addMinuteServiceToReservationDto.ReservationId,
                MinuteServiceId = addMinuteServiceToReservationDto.MinuteServiceId,
                Duration = addMinuteServiceToReservationDto.Duration,
            };
        }

        public GetMinuteServiceToReservationDto MinuteServiceToReservationToGetMinuteServiceToReservationDto(
            MinuteServiceToReservation minuteServiceToReservation)
        {
            return new GetMinuteServiceToReservationDto()
            {
                ReservationId = minuteServiceToReservation.ReservationId,
                MinuteServiceId = minuteServiceToReservation.MinuteServiceId,
                Duration = minuteServiceToReservation.Duration,
                Cost = minuteServiceToReservation.Cost
            };
        }
    }
}
