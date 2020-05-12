using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iReception.Repository
{
    public class MinuteServiceToReservationRepository : IMinuteServicesToReservationRepository
    {

        private readonly iReceptionDbContext _db;
        private readonly IMinuteServiceRepository _minuteServiceRepository;

        public MinuteServiceToReservationRepository(iReceptionDbContext db,
            IMinuteServiceRepository minuteServiceRepository)
        {
            _db = db;
            _minuteServiceRepository = minuteServiceRepository;
        }

        public async Task<int> AddAsync(MinuteServiceToReservation minuteServiceToReservation)
        {
            if (minuteServiceToReservation.MinuteServiceId == 0)
            {
                return minuteServiceToReservation.ReservationId;
            }
            var minuteService = await _minuteServiceRepository.GetAsync(minuteServiceToReservation.MinuteServiceId);
            minuteServiceToReservation.Cost = minuteServiceToReservation.Duration * minuteService.PricePerMinute;

            await _db.MinuteServicesToReservation.AddAsync(minuteServiceToReservation);
            await _db.SaveChangesAsync();

            return minuteServiceToReservation.MinuteServiceId;
        }

        public async Task<IEnumerable<MinuteServiceToReservation>> ListAssignedAsync(int reservationId)
        {
            var assignedServices = await _db.MinuteServicesToReservation.Where(mstr => mstr.ReservationId == reservationId).ToListAsync();

            return assignedServices;
        }

        public async Task<int> DeleteAsync(int reservationId, int minuteServiceId)
        {
            var assignedService = await _db.MinuteServicesToReservation.FindAsync(reservationId, minuteServiceId);

            _db.MinuteServicesToReservation.Remove(assignedService);
            await _db.SaveChangesAsync();

            return minuteServiceId;
        }
    }
}
