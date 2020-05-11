using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace iReception.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly iReceptionDbContext _db;

        public ReservationRepository(iReceptionDbContext db)
        {
            _db = db;
        }


        public async Task<int> AddAsync(Reservation reservation)
        {
            var reservedRoom = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == reservation.RoomId);
            var reservationBalance = 
                (reservation.EndDate - reservation.StartDate).Days * reservedRoom.PricePerDay;
            reservation.Balance = reservationBalance;

            reservation.IsPaid = false;
            reservation.Active = false;
            reservation.IsDeleted = false;

            await _db.Reservations.AddAsync(reservation);
            await _db.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<Reservation> GetAsync(int id)
        {
            var requestedReservation = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            return requestedReservation;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var reservationToDelete = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            reservationToDelete.IsDeleted = true;

            return reservationToDelete.Id;
        }

        public async Task<IEnumerable<Reservation>> FilterAsync(FilterReservationDto filterReservationDto)
        {
            var filters = 0;
            var activeFiltered = false;
            var paidFiltered = false;

            var result = new List<Reservation>();

            var minId = filterReservationDto.MinId ?? 0;
            var maxId = filterReservationDto.MaxId ?? 0;
            if (minId > 0 || maxId > 0)
            {
                var filteredMinIds = await _db.Reservations.Where(r => r.Id >= minId).ToListAsync();
                var filteredMaxIds = await _db.Reservations.Where(r => r.Id <= maxId).ToListAsync();

                if (minId > 0 && maxId == 0)
                {
                    result.AddRange(filteredMinIds);
                }
                else if (maxId > 0 && minId == 0)
                {
                    result.AddRange(filteredMaxIds);
                }

                result.AddRange(filteredMaxIds.Intersect(filteredMinIds));
                filters += 1;
            }

            var minBalance = filterReservationDto.MinBalance ?? 0;
            var maxBalance = filterReservationDto.MaxBalance ?? 0;
            if (minId > 0 || maxId > 0)
            {
                var filteredMinBalance = await _db.Reservations.Where(r => r.Balance >= minBalance).ToListAsync();
                var filteredMaxBalance = await _db.Reservations.Where(r => r.Balance <= maxBalance).ToListAsync();

                if (minBalance > 0 && maxBalance == 0)
                {
                    result.AddRange(filteredMinBalance);
                }
                else if (maxBalance > 0 && minBalance == 0)
                {
                    result.AddRange(filteredMaxBalance);
                }

                result.AddRange(filteredMaxBalance.Intersect(filteredMinBalance));
                filters += 1;
            }

            var minDate = filterReservationDto.MinDate;
            var maxDate = filterReservationDto.MaxDate;
            if (minDate != null || maxDate != null)
            {
                var filteredMinDates = await _db.Reservations.Where(r => r.StartDate >= minDate).ToArrayAsync();
                var filteredMaxDates = await _db.Reservations.Where(r => r.EndDate <= maxDate).ToArrayAsync();

                if (minDate != null && maxDate == null)
                {
                    result.AddRange(filteredMinDates);
                }
                else if (minDate == null)
                {
                    result.AddRange(filteredMaxDates);
                }

                result.AddRange(filteredMaxDates.Intersect(filteredMinDates));
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterReservationDto.Room))
            {
                var filteredRooms = _db.Reservations.Where(r => EF.Functions.Like(r.Room.Number, $"%{filterReservationDto.Room}%"));
                result.AddRange(filteredRooms);
                filters += 1;
            }

            if (filterReservationDto.IsActive)
            {
                var activeReservations = await _db.Reservations.Where(r => r.Active).ToListAsync();
                result.AddRange(activeReservations);
                if (!activeFiltered) { activeFiltered = true; }
            }
            if (filterReservationDto.IsNotActive)
            {
                var notActiveReservations = await _db.Reservations.Where(r => !r.Active).ToListAsync();
                result.AddRange(notActiveReservations);
                if (!activeFiltered) { activeFiltered = true; }
            }

            if (filterReservationDto.IsPaid)
            {
                var paidReservations = await _db.Reservations.Where(r => r.IsPaid).ToListAsync();
                result.AddRange(paidReservations);
                if (!paidFiltered) { paidFiltered = true; }
            }
            if (filterReservationDto.IsNotPaid)
            {
                var notPaidReservations = await _db.Reservations.Where(r => !r.IsPaid).ToListAsync();
                result.AddRange(notPaidReservations);
                if (!paidFiltered) { paidFiltered = true; }
            }

            if (activeFiltered) { filters += 1; }
            if (paidFiltered) { filters += 1; }

            var groupedResults = result.GroupBy(r => r);
            var finalResult = new List<Reservation>();
            foreach (var gr in groupedResults)
            {
                if (gr.Count() == filters) { finalResult.Add(gr.Key); }
            }

            return finalResult;
        }

        public async Task<IEnumerable<Reservation>> ListAsync()
        {
            var allReservations = await _db.Reservations.ToListAsync();
            return allReservations;
        }

        public async Task<int> PayAsync(int id)
        {
            var reservation = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation.IsPaid)
            {
                throw new InvalidOperationException("Reservation is already paid");
            }

            reservation.IsPaid = true;
            return reservation.Id;
        }

    }
}
