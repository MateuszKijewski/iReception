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
    public class ReservationRepositoryTests : InMemoryDatabaseFixture
    {
        [Fact]
        public async Task ShouldAddNewReservation()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var roomToReturn = new Room
            {
                PricePerDay = 10
            };
            await context.Rooms.AddAsync(roomToReturn);
            await context.SaveChangesAsync();

            var repository = new ReservationRepository(context);
            var convertedDto = new Reservation
            {
                StartDate = new DateTime(2020, 7, 23),
                EndDate = new DateTime(2020, 7, 30),
                RoomId = 1,
                ClientId = 1,
            };

            //Act
            var actual = await repository.AddAsync(convertedDto);
            var actualObject = await context.Reservations.OrderByDescending(r => r.Id).FirstOrDefaultAsync();
            var expected = 1;
            var expectedBalance = 
                (convertedDto.EndDate - convertedDto.StartDate).Days * roomToReturn.PricePerDay;


            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expectedBalance, actualObject.Balance);
            Assert.False(actualObject.IsPaid);
            Assert.False(actualObject.Active);
            Assert.False(actualObject.IsDeleted);

        }

        [Fact]
        public async Task ShouldThrowExceptionWhenReservationIsPaid()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var paidReservation = new Reservation
            {
                IsPaid = true
            };
            var repository = new ReservationRepository(context);
            await context.Reservations.AddAsync(paidReservation);
            await context.SaveChangesAsync();

            //Act
            var expectedExceptionMessage = "Reservation is already paid";

            //Assert
            var actualException = await Assert.ThrowsAsync<InvalidOperationException>
                (async () => await repository.PayAsync(1));
            actualException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
            
        }
    }
}
