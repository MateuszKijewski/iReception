using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using iReception.Models.Converters;
using iReception.Models.Enums;
using iReception.Repository.Interfaces;
using Moq;
using Xunit;

namespace iReception.Test.ReservationTests
{
    public class ReservationConverterTests
    {
        [Fact]
        public void ShouldReturnProperEnumElement()
        {
            //Arrange
            //Closing
            var closingMockTime = new DateTime(2020, 7, 29);
            var closingTimeProviderMock = new Mock<ITimeProvider>();
            closingTimeProviderMock
                .Setup(tp => tp.GetCurrentTime())
                .Returns(closingMockTime);

            var closingConverter = new ReservationConverter(
                null,
                null,
                closingTimeProviderMock.Object
                );
            var closingEndDate = new DateTime(2020, 7, 30);

            //Ongoing
            var ongoingMockTime = new DateTime(2020, 7, 23);
            var ongoingTimeProviderMock = new Mock<ITimeProvider>();
            ongoingTimeProviderMock
                .Setup(tp => tp.GetCurrentTime())
                .Returns(ongoingMockTime);

            var ongoingConverter = new ReservationConverter(
                null,
                null,
                ongoingTimeProviderMock.Object
            );
            var ongoingStartDate = new DateTime(2020, 7, 20);
            var ongoingEndDate = new DateTime(2020, 7, 30);

            //Approaching
            var approachingMockTime = new DateTime(2020, 7, 23);
            var approachingTimeProviderMock = new Mock<ITimeProvider>();
            approachingTimeProviderMock
                .Setup(tp => tp.GetCurrentTime())
                .Returns(approachingMockTime);

            var approachingConverter = new ReservationConverter(
                null,
                null,
                approachingTimeProviderMock.Object
            );
            var approachingStartDate = new DateTime(2020, 7, 25);

            //Today
            var todayMockTime = new DateTime(2020, 7, 20);
            var todayTimeProviderMock = new Mock<ITimeProvider>();
            todayTimeProviderMock
                .Setup(tp => tp.GetCurrentTime())
                .Returns(todayMockTime);

            var todayConverter = new ReservationConverter(
                null,
                null,
                todayTimeProviderMock.Object
            );
            var todayStartDate = new DateTime(2020, 7, 20);
            var todayEndDate = new DateTime(2020, 7, 27);

            //Finished
            var finishedMockTime = new DateTime(2020, 7, 20);
            var finishedTimeProviderMock = new Mock<ITimeProvider>();
            finishedTimeProviderMock
                .Setup(tp => tp.GetCurrentTime())
                .Returns(finishedMockTime);

            var finishedConverter = new ReservationConverter(
                null,
                null,
                finishedTimeProviderMock.Object
            );
            var finishedStartDate = new DateTime(2020, 7, 10);
            var finishedEndDate = new DateTime(2020, 7, 20);

            //New
            var newMockTime = new DateTime(2020, 7, 20);
            var newTimeProviderMock = new Mock<ITimeProvider>();
            newTimeProviderMock
                .Setup(tp => tp.GetCurrentTime())
                .Returns(newMockTime);

            var newConverter = new ReservationConverter(
                null,
                null,
                newTimeProviderMock.Object
            );
            var newStartDate = new DateTime(2020, 10, 20);
            var newEndDate = new DateTime(2020, 11, 1);

            //Act
            var actualClosing = closingConverter.AssignStatus(new DateTime(), closingEndDate);
            var actualOngoing = ongoingConverter.AssignStatus(ongoingStartDate, ongoingEndDate);
            var actualApproaching = approachingConverter.AssignStatus(approachingStartDate, new DateTime());
            var actualToday = todayConverter.AssignStatus(todayStartDate, todayEndDate);
            var actualFinished = finishedConverter.AssignStatus(finishedStartDate, finishedEndDate);
            var actualNew = newConverter.AssignStatus(newStartDate, newEndDate);

            //Assert
            actualClosing.Should().BeEquivalentTo(ReservationStatus.Closing);
            actualOngoing.Should().BeEquivalentTo(ReservationStatus.Ongoing);
            actualApproaching.Should().BeEquivalentTo(ReservationStatus.Approaching);
            actualToday.Should().BeEquivalentTo(ReservationStatus.Today);
            actualFinished.Should().BeEquivalentTo(ReservationStatus.Finished);
            actualNew.Should().BeEquivalentTo(ReservationStatus.New);

        }
    }
}
