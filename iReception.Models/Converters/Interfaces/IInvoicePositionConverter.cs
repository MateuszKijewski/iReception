using System;
using System.Collections.Generic;
using System.Text;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters.Interfaces
{
    public interface IInvoicePositionConverter
    {
        GetInvoicePositionDto ReservationToGetInvoicePositionDto(Reservation reservation);
        GetInvoicePositionDto MinuteServiceToGetInvoicePositionDto(MinuteServiceToReservation minuteServiceToReservation);
    }
}
