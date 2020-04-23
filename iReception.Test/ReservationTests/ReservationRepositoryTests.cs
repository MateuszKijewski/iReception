using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository;
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
            var repository = new ReservationRepository(context);
            var convertedDto = new Reservation
            {
                ReservationStartDate = new DateTime(2020, 7, 23),
                ReservationEndDate = new DateTime(2020, 7, 30),
                RoomId = 1
            };
            //Act


            //Assert
        }

    }
}
