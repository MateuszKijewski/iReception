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
        public async Task ShouldAssignServicesToReservation()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var returnedMinuteService = new MinuteService
            {
                PricePerMinute = 10
            };
            var msRepository = new Mock<IMinuteServiceRepository>();
            msRepository
                .Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(returnedMinuteService);
            var repository = new MinuteServiceToReservationRepository(context, msRepository.Object);


            var reservationId = 1;
            var servicesWithDuration = new Dictionary<int, int>
            {
                {12, 30},
                {14, 60}
            };

            //Act
            var actual = await repository.AddAsync(reservationId, servicesWithDuration);
            var expected = servicesWithDuration.Keys.ToArray();
            var actualCost = (await context.MinuteServicesToReservation.FirstAsync(
                mstr => mstr.MinuteServiceId == 12)).Cost;
            var expectedCost = servicesWithDuration[12] * returnedMinuteService.PricePerMinute;

            //Assert
            actual.Should().BeEquivalentTo(expected);
            Assert.Equal(expectedCost, actualCost);
        }

        [Fact]
        public async Task ShouldAssignNothingAndReturnReservationId()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new MinuteServiceToReservationRepository(context, null);
            var reservationId = 1;

            //Act
            var actual = await repository.AddAsync(reservationId, null);
            var expected = reservationId;

            //Assert
            actual.Should().BeEquivalentTo(expected);
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
