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
            // Arrange
            var context = new iReceptionDbContext(DbOptions);
            var repository = new RoomToServiceRepository(context);

            var existingRtms = new RoomToService()
            {
                RoomId = 18,
                ServiceId = 21
            };
            await context.RoomToServices.AddAsync(existingRtms);

            var assignServiceDtos = new List<AssignServiceDto>
            {
                new AssignServiceDto {RoomId = 1, ServiceId = 2},
                new AssignServiceDto {RoomId = 1, ServiceId = 3}
            };
            // Act
            var actual = await repository.AssignAsync(assignServiceDtos);
            var expected = assignServiceDtos.Select(asd => asd.ServiceId);
            await context.SaveChangesAsync();



            // Assert
            Assert.Equal(expected, actual);
            (await context.RoomToServices.FindAsync(18, 21)).Should().BeNull();

        }
    }
}
