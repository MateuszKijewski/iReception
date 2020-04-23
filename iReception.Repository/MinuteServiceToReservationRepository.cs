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

        public async Task<int[]> AddAsync(int reservationId, Dictionary<int, int> servicesWithDuration)
        {
            if (servicesWithDuration != null)
            {
                foreach(KeyValuePair<int, int> entry in servicesWithDuration)
                {
                    var minuteService = await _minuteServiceRepository.GetAsync(entry.Key);
                    var minuteServiceToReservation = new MinuteServiceToReservation
                    {
                        ReservationId = reservationId,
                        MinuteServiceId = entry.Key,
                        Duration = entry.Value,
                        Cost = entry.Value * minuteService.PricePerMinute
                    };
                    await _db.MinuteServicesToReservation.AddAsync(minuteServiceToReservation);
                }

                await _db.SaveChangesAsync();
                return servicesWithDuration.Keys.ToArray();
            }

            return new[]{reservationId};
        }

        public async Task<int[]> ListAssignedAsync()
        {
            var assignedServicesIds = _db.MinuteServicesToReservation.Select(mstr => mstr.MinuteServiceId);
            return await assignedServicesIds.ToArrayAsync();
        }
         
        public async Task<int> DeleteAsync(int reservationId, int minuteServiceId)
        {
            var connectionToDelete = await _db.MinuteServicesToReservation.FindAsync(reservationId,
                                                                                        minuteServiceId);
            _db.Remove(connectionToDelete);
            await _db.SaveChangesAsync();

            return minuteServiceId;
        }
    }
}
