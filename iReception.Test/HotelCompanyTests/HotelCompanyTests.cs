using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository;
using Xunit;

namespace iReception.Test.HotelCompanyTests
{
    public class HotelCompanyTests : InMemoryDatabaseFixture
    {
        [Fact]
        public async void ShouldThrowExceptionIfCompanyObjectExists()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var hotelCompany = new HotelCompany();
            var repository = new HotelCompanyRepository(context);

            await context.AddAsync(hotelCompany);
            await context.SaveChangesAsync();

            //Act
            var expectedExceptionMessage = "HotelCompany object is already created";

            //Assert
            var actualException = await Assert.ThrowsAsync<InvalidOperationException>
                (async () => await repository.AddAsync());
            actualException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

    }
}
