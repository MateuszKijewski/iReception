using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using iReception.DataAccess;
using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using iReception.Services;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace iReception.Test.RoomTests
{
    public class RoomServiceTests : InMemoryDatabaseFixture
    {
        private readonly ITestOutputHelper _output;

        public RoomServiceTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ShouldAssignServicesToRoom()
        {
            //Arrange
            var repositoryMock = new Mock<IRoomToServiceRepository>();
            repositoryMock
                .Setup(r => r.AssignAsync(It.IsAny<int>(), It.IsAny<int[]>()))
                .ReturnsAsync(new int[] {1, 2});
            var service = new RoomService(
                null,
                null,
                null,
                roomToServiceRepository:repositoryMock.Object,
                null
                );

            int roomId = 1;
            int[] serviceIds = {1, 2};

            //Act
            var actual = await service.AssignServicesAsync(roomId, serviceIds);

            //Assert
            actual.Should().Equal(serviceIds);
        }

        [Fact]
        public async Task ShouldRemoveCurrentServices()
        {
            //Arrange
            var repositoryMock = new Mock<IRoomToServiceRepository>();
            repositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = new RoomService(
                null,
                null,
                null,
                roomToServiceRepository: repositoryMock.Object,
                null
            );
            int roomId = 1;
            //Act
            var actual = await service.AssignServicesAsync(roomId, null);
            
            //Assert
            Assert.Equal(1, actual[0]);
        }

        [Fact]
        public async Task ShouldListAssignedServices()
        {
            //Arrange
            var context = new iReceptionDbContext(DbOptions);

            var rtsRepositoryMock = new Mock<IRoomToServiceRepository>();
            rtsRepositoryMock
                .Setup(r => r.ListAssignedAsync(It.IsAny<int>()))
                .ReturnsAsync(new int[] { 1, 2 });
            var serviceRepositoryMock = new Mock<IServiceRepository>();
            serviceRepositoryMock
                .Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(new Service()
                {
                    Id = 1,
                    Description = "test",
                    IsAvailable = true,
                    Name = "test"
                });
            var serviceConverterMock = new Mock<IServiceConverter>();
            serviceConverterMock
                .Setup(c => c.ServiceToGetServiceDto(It.IsAny<Service>()))
                .Returns(new GetServiceDto
                {
                    Id = 1,
                    Name = "test",
                    Description = "test",
                    IsAvailable = true
                });


            var serviceService = new ServiceService(
                serviceConverterMock.Object,
                serviceRepositoryMock.Object
            );
            var roomService = new RoomService(
                null,
                null,
                null,
                roomToServiceRepository: rtsRepositoryMock.Object,
                serviceService
            );
            
            int roomId = 1;

            //Act
            var actual = await roomService.ListAssignedServicesAsync(roomId);
            var expected = new List<GetServiceDto>
            {
                new GetServiceDto()
                    {Id = 1, Name = "test", Description = "test", IsAvailable = true},
                new GetServiceDto()
                    {Id = 1, Name = "test", Description = "test", IsAvailable = true}
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
