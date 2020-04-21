using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iReception.Repository
{
    public class RoomToMinuteServiceRepository : IRoomToMinuteServiceRepository
    {
        private readonly iReceptionDbContext _db;

        public RoomToMinuteServiceRepository(iReceptionDbContext db)
        {
            _db = db;
        }

        public async Task<int[]> AssignAsync(int roomId, int[] serviceIds)
        {
            var existingConnections = _db.RoomToMinuteServices.Where(r => r.RoomId == roomId);
            _db.RoomToMinuteServices.RemoveRange(existingConnections);

            foreach (var minuteServiceId in serviceIds)
            {
                var relation = new RoomToMinuteService
                {
                    RoomId = roomId,
                    MinuteServiceId = minuteServiceId
                };
                await _db.RoomToMinuteServices.AddAsync(relation);
            }

            await _db.SaveChangesAsync();
            return serviceIds;
        }

        public async Task<int> DeleteAsync(int roomId)
        {
            var existingConnections = _db.RoomToMinuteServices.Where(r => r.RoomId == roomId);
            _db.RoomToMinuteServices.RemoveRange(existingConnections);
            await _db.SaveChangesAsync();

            return roomId;
        }

        public async Task<int[]> ListAssignedAsync(int roomId)
        {
            var assignedServices = await _db.RoomToMinuteServices.Where(rtms => rtms.RoomId == roomId).ToListAsync();
            return assignedServices.Select(s => s.MinuteServiceId).ToArray();
        }
    }
}
