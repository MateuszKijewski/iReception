using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace iReception.Test.ReservationTests
{
    public class MinuteServiceToReservationTests : InMemoryDatabaseFixture
    {

        [Fact]
        public async Task ShouldAssignServiceToReservation()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var returnedMinuteService = new MinuteService
            {
                Id = 1,
                PricePerMinute = 10
            };
            var msRepository = new Mock<IMinuteServiceRepository>();
            msRepository
                .Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(returnedMinuteService);
            var repository = new MinuteServiceToReservationRepository(context, msRepository.Object);


            var convertedMinuteServiceToReservation = new MinuteServiceToReservation
            {
                ReservationId = 1,
                MinuteServiceId = 12,
                Duration = 30
            };

            //Act
            var actual = await repository.AddAsync(convertedMinuteServiceToReservation);
            var expected = convertedMinuteServiceToReservation.MinuteServiceId;
            var actualCost = (await context.MinuteServicesToReservation.FirstAsync(
                mstr => mstr.MinuteServiceId == 12)).Cost;
            var expectedCost = convertedMinuteServiceToReservation.Duration * returnedMinuteService.PricePerMinute;

            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expectedCost, actualCost);
        }

        [Fact]
        public async Task ShouldAssignNothingAndReturnReservationId()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new MinuteServiceToReservationRepository(context, null);
            var convertedMinuteServiceToReservation = new MinuteServiceToReservation
            {
                ReservationId = 1,
                MinuteServiceId = 0,
                Duration = 30
            };

            //Act
            var actual = await repository.AddAsync(convertedMinuteServiceToReservation);
            var expected = convertedMinuteServiceToReservation.ReservationId;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldRemoveGivenConnection()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new MinuteServiceToReservationRepository(context, null);
            var connectionToDelete = new MinuteServiceToReservation
            {
                ReservationId = 1,
                MinuteServiceId = 2
            };
            await context.MinuteServicesToReservation.AddAsync(connectionToDelete);
            await context.SaveChangesAsync();

            //Act
            var actual = await repository.DeleteAsync(connectionToDelete.ReservationId, 
                                                        connectionToDelete.MinuteServiceId);
            var expected = connectionToDelete.MinuteServiceId;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
