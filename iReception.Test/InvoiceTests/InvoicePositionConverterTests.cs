using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using iReception.DataAccess;
using iReception.Models.Converters;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;
using Xunit;

namespace iReception.Test.InvoiceTests
{
    public class InvoicePositionConverterTests : InMemoryDatabaseFixture
    {
        [Fact]
        public void ShouldReturnInvoicePositionFromReservation()
        {
            //Arrange
            var reservation = new Reservation()
            {
                StartDate = new DateTime(2020, 7, 12),
                EndDate = new DateTime(2020, 7, 20),
                Room = new Room(){Number = "12A", PricePerDay = 40},
            };
            var converter = new InvoicePositionConverter();

            //Act
            var actualInvoicePosition = converter.ReservationToGetInvoicePositionDto(reservation);
            var expectedInvoicePosition = new GetInvoicePositionDto()
            {
                Name = "12A / 12.07.2020 - 20.07.2020",
                PriceBrutto = 320,
                VatRate = 8,
                PriceNetto = 294.40M,
                Amount = 1,
                VatValue = 25.60M
            };

            //Assert
            actualInvoicePosition.Should().BeEquivalentTo(expectedInvoicePosition);
        }

        [Fact]
        public void ShouldReturnInvoicePositionFromMinuteService()
        {
            //Arrange
            var minuteServiceToReservation = new MinuteServiceToReservation()
            {
                MinuteService = new MinuteService(){Name = "Cycling"},
                Cost = 120,
                Duration = 60
            };
            var converter = new InvoicePositionConverter();

            //Act
            var actualInvoicePosition = converter.MinuteServiceToGetInvoicePositionDto(minuteServiceToReservation);
            var expectedInvoicePosition = new GetInvoicePositionDto()
            {
                Name = "Cycling / 60 minutes",
                PriceBrutto = 120,
                VatRate = 23,
                PriceNetto = 92.40M,
                Amount = 1,
                VatValue = 27.60M
            };

            //Assert
            actualInvoicePosition.Should().BeEquivalentTo(expectedInvoicePosition);
        }
    }
}
