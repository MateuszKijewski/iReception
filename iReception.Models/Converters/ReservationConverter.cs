using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Entities;
using iReception.Models.Enums;
using iReception.Repository.Interfaces;

namespace iReception.Models.Converters
{
    public class ReservationConverter : IReservationConverter
    {
        private readonly IClientConverter _clientConverter;
        private readonly IRoomConverter _roomConverter;
        private readonly ITimeProvider _timeProvider;

        public ReservationConverter(IClientConverter clientConverter,
                                    IRoomConverter roomConverter,
                                    ITimeProvider timeProvider)
        {
            _clientConverter = clientConverter;
            _roomConverter = roomConverter;
            _timeProvider = timeProvider;
        }

        public Reservation AddReservationDtoToReservation(AddReservationDto addReservationDto)
        {
            return new Reservation
            {
                ReservationStartDate = addReservationDto.ReservationStartDate,
                ReservationEndDate = addReservationDto.ReservationEndDate,
                RoomId = addReservationDto.RoomId,
                ClientId =  addReservationDto.ClientId
            };
        }

        public GetReservationDto ReservationToGetReservationDto(Reservation reservation)
        {
            var status = AssignStatus(reservation.ReservationStartDate, reservation.ReservationEndDate);
            return new GetReservationDto
            {
                Id = reservation.Id,
                Balance = reservation.Balance,
                ReservationStartDate = reservation.ReservationStartDate,
                ReservationEndDate = reservation.ReservationEndDate,
                Status = status,
                Paid = reservation.Paid,
                Room = _roomConverter.RoomToGetRoomDto(reservation.Room),
                Client = _clientConverter.ClientToGetClientDto(reservation.Client)
            };
        }

        public ReservationStatus AssignStatus(DateTime start, DateTime end)
        {
            var today = _timeProvider.GetCurrentTime();
            var test = Math.Abs((start - today).Days);
            var test2 = Math.Abs((end - today).Days);

            if (end == today)
            {
                return ReservationStatus.Finished;
            }

            if (start == today)
            {
                return ReservationStatus.Today;
            }


            if (Math.Abs((start - today).Days) < 3)
            {
                return ReservationStatus.Approaching;
            }

            if (Math.Abs((end - today).Days) < 2)
            {
                return ReservationStatus.Closing;
            }

            if (today.Ticks > start.Ticks && today.Ticks < end.Ticks)
            {
                return ReservationStatus.Ongoing;
            }

            return ReservationStatus.New;
        }
    }
}
