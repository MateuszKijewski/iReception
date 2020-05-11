using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;

namespace iReception.Models.Converters
{
    public class InvoicePositionConverter : IInvoicePositionConverter
    {
        public GetInvoicePositionDto ReservationToGetInvoicePositionDto(Reservation reservation)
        {
            var name = $"{reservation.Room.Number} / {reservation.StartDate.ToShortDateString()} - {reservation.EndDate.ToShortDateString()}";
            var priceBrutto = (reservation.EndDate - reservation.StartDate).Days * reservation.Room.PricePerDay;
            var priceNetto = priceBrutto * 0.92M;
            var vatValue = priceBrutto - priceNetto;

            return new GetInvoicePositionDto()
            {
                Name = name,
                PriceBrutto = priceBrutto,
                PriceNetto = priceNetto,
                VatRate = 8,
                Amount = 1,
                VatValue = vatValue
            };
        }

        public GetInvoicePositionDto MinuteServiceToGetInvoicePositionDto(MinuteServiceToReservation minuteServiceToReservation)
        {
            var name = $"{minuteServiceToReservation.MinuteService.Name} / {minuteServiceToReservation.Duration} minutes";
            var priceBrutto = minuteServiceToReservation.Cost;
            var priceNetto = priceBrutto * 0.77M;
            var vatValue = priceBrutto - priceNetto;

            return new GetInvoicePositionDto()
            {
                Name = name,
                PriceBrutto = priceBrutto,
                PriceNetto = priceNetto,
                VatRate = 23,
                Amount = 1,
                VatValue = vatValue
            };
        }
    }
}
