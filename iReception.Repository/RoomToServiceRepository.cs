using iReception.DataAccess;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository
{
    public class RoomToServiceRepository : IRoomToServiceRepository
    {
        private readonly iReceptionDbContext _db;

        public RoomToServiceRepository(iReceptionDbContext db)
        {
            _db = db;
        }

        public async Task<int[]> AssignAsync(int roomId, int[] serviceIds)
        {
            var currentRelations = _db.RoomToServices.Where(rts => rts.RoomId == roomId);
            _db.RemoveRange(currentRelations);
            foreach(var serviceId in serviceIds)
            {
                var relation = new RoomToService
                {
                    RoomId = roomId,
                    ServiceId = serviceId
                };

                await _db.RoomToServices.AddAsync(relation);
                
            }
            await _db.SaveChangesAsync();
            return serviceIds;
        }
        public async Task<int> DeleteAsync(int roomId)
        {
            var currentRelations = _db.RoomToServices.Where(rts => rts.RoomId == roomId);
            _db.RemoveRange(currentRelations);            
            await _db.SaveChangesAsync();
            
            return roomId;
        }

        public async Task<int[]> ListAssignedAsync(int roomId)
        {
            var assignedServices = await _db.RoomToServices.Where(rts => rts.RoomId == roomId).ToListAsync();
            return assignedServices.Select(s => s.ServiceId).ToArray();
        }
    }
}
