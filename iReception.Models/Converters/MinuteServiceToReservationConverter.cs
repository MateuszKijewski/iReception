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
            var minuteServiceId = addMinuteServiceToReservationDto.MinuteServiceId ?? 0;
            return new MinuteServiceToReservation()
            {
                ReservationId = addMinuteServiceToReservationDto.ReservationId,
                MinuteServiceId = minuteServiceId,
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
