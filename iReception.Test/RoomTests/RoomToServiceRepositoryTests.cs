using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Entities;
using Xunit;
using iReception.Repository;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;
using FluentAssertions;

namespace iReception.Test.RoomTests
{
    public class RoomToServiceRepositoryTests : InMemoryDatabaseFixture
    {
        private readonly ITestOutputHelper _output;

        public RoomToServiceRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ShouldAssignServicesToRoomAndRemoveCurrentServices()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToServiceRepository(context);

            var existingServices = new RoomToService()
            {
                RoomId = 1,
                ServiceId = 21
            };
            await context.RoomToServices.AddAsync(existingServices);
            await context.SaveChangesAsync();

            int roomId = 1;
            int[] serviceIds = {2, 3, 4};

            //Act
            var actual = await repository.AssignAsync(roomId, serviceIds);
            var expected = serviceIds;
            var deletedRecord = await context.RoomToServices.FindAsync(1, 21);


            //Assert
            actual.Should().BeEquivalentTo(expected);
            deletedRecord.Should().BeNull();

        }

        [Fact]
        public async Task ShouldClearExistingConnections()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToServiceRepository(context);

            var existingServices = new RoomToService()
            {
                RoomId = 1,
                ServiceId = 21
            };
            await context.RoomToServices.AddAsync(existingServices);
            await context.SaveChangesAsync();

            //Act
            await repository.DeleteAsync(1);
            var actual = context.RoomToServices.ToList();

            //Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldListServicesAssignedToRoom()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToServiceRepository(context);

            List<RoomToService> existingServices = new List<RoomToService>
            {
                new RoomToService() {RoomId = 1, ServiceId = 2},
                new RoomToService() {RoomId = 1, ServiceId = 3}
            };
            await context.RoomToServices.AddRangeAsync(existingServices);
            await context.SaveChangesAsync();

            //Act
            var actual = await repository.ListAssignedAsync(1);
            var expected = existingServices.Select(es => es.ServiceId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
