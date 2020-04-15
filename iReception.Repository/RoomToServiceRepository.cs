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

        public async Task<int> AssignAsync(IEnumerable<AssignServiceDto> assignServiceDtos)
        {
            var currentRelations = _db.RoomToServices.Where(rts => rts.RoomId == assignServiceDtos.ToList()[0].RoomId);
            _db.RemoveRange(currentRelations);
            foreach(var item in assignServiceDtos)
            {
                var relation = new RoomToService
                {
                    RoomId = item.RoomId,
                    ServiceId = item.ServiceId
                };

                await _db.RoomToServices.AddAsync(relation);
                await _db.SaveChangesAsync();
            }
            return 1;
        }
        public async Task<int> AssignAsync(int roomId)
        {
            var currentRelations = _db.RoomToServices.Where(rts => rts.RoomId == roomId);
            _db.RemoveRange(currentRelations);            
            await _db.SaveChangesAsync();
            
            return 1;
        }

        public async Task<int[]> ListAssignedAsync(int roomId)
        {
            var assignedServices = await _db.RoomToServices.Where(rts => rts.RoomId == roomId).ToListAsync();
            return assignedServices.Select(s => s.ServiceId).ToArray();
        }
    }
}
