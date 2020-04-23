using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters.Interfaces
{
    public interface IReservationConverter
    {
        Reservation AddReservationDtoToReservation(AddReservationDto addReservationDto);
        GetReservationDto ReservationToGetReservationDto(Reservation reservation);

    }
}
