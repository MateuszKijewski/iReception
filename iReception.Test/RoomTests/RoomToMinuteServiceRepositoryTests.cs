using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository;
using Xunit;
using Xunit.Abstractions;

namespace iReception.Test.RoomTests
{
    public class RoomToMinuteServiceRepositoryTests : InMemoryDatabaseFixture
    {
        private readonly ITestOutputHelper _output;

        public RoomToMinuteServiceRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ShouldAssignMinuteServicesToRoomAndRemoveCurrentConnections()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToMinuteServiceRepository(context);

            var existingMinuteServices = new RoomToMinuteService()
            {
                RoomId = 1,
                MinuteServiceId = 21
            };
            await context.RoomToMinuteServices.AddAsync((existingMinuteServices));
            await context.SaveChangesAsync();

            int roomId = 1;
            int[] serviceIds = {2, 3, 4};

            //Act
            var actual = await repository.AssignAsync(roomId, serviceIds);
            var expected = serviceIds;
            var deletedRecord = await context.RoomToServices.FindAsync(1, 21);

            //Assert
            actual.Should().BeEquivalentTo(serviceIds);
            deletedRecord.Should().BeNull();
        }

        [Fact]
        public async Task ShouldClearExistingConnections()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToMinuteServiceRepository(context);

            var existingMinuteService = new RoomToMinuteService
            {
                RoomId = 1,
                MinuteServiceId = 21
            };
            await context.RoomToMinuteServices.AddAsync(existingMinuteService);
            await context.SaveChangesAsync();

            //Act
            await repository.DeleteAsync(1);
            var actual = context.RoomToMinuteServices.ToList();

            //Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldListAllExistingConnections()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToMinuteServiceRepository(context);

            var existingMinuteServices = new List<RoomToMinuteService>
            {
                new RoomToMinuteService{RoomId = 1, MinuteServiceId = 2},
                new RoomToMinuteService{RoomId = 1, MinuteServiceId = 3}
            };
            await context.RoomToMinuteServices.AddRangeAsync(existingMinuteServices);
            await context.SaveChangesAsync();

            //Act
            var actual = await repository.ListAssignedAsync(1);
            var expected = existingMinuteServices.Select(ms => ms.MinuteServiceId);

            //Assert
            actual.Should().BeEquivalentTo(expected);

        }
    }
}
