using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly iReceptionDbContext _db;
        private readonly IBuildingRepository _buildingRepository;

        public RoomRepository(iReceptionDbContext db,
                            IBuildingRepository buildingRepository)
        {
            _db = db;
            _buildingRepository = buildingRepository;
        }

        public async Task<int> AddAsync(Room room)
        {
            room.IsAvailable = true;
            room.IsDamaged = false;
            room.IsClean = true;
            room.IsRent = false;
            room.IsDeleted = false;
            room.Building = await _buildingRepository.GetAsync(room.BuildingId);
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
            return room.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var roomToDelete = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (roomToDelete != null)
            {
                if (!roomToDelete.IsDeleted)
                {
                    roomToDelete.IsDeleted = true;
                    _db.Rooms.Update(roomToDelete);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Room is already deleted");
            }
            throw new NullReferenceException("Requested room does not exist");
        }

        public async Task<IEnumerable<Room>> FilterAsync(FilterRoomDto filterRoomDto)
        {
            int filters = 0;
            bool availableFiltered = false;
            bool damagedFiltered = false;
            bool cleanFiltered = false;
            bool rentFiltered = false;

            List<Room> result = new List<Room>();

            int minId = filterRoomDto.MinId ?? 0;
            int maxId = filterRoomDto.MaxId ?? 0;               

            if (minId > 0 || maxId > 0)
            {
                var filteredMinIds = await _db.Rooms.Where(r => r.Id >= minId).ToListAsync();
                var filteredMaxIds = await _db.Rooms.Where(r => r.Id <= maxId).ToListAsync();

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

            if (!string.IsNullOrEmpty(filterRoomDto.Number))
            {
                var filteredNumbers = await _db.Rooms.Where(r => EF.Functions.Like(r.Number, $"%{filterRoomDto.Number}%")).ToListAsync();
                result.AddRange(filteredNumbers);
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterRoomDto.Standard.ToString()))
            {
                var filteredStandards = await _db.Rooms.Where(r => r.Standard == filterRoomDto.Standard).ToListAsync();
                result.AddRange(filteredStandards);
                filters += 1;
            }

            int minFloor = filterRoomDto.MinFloor ?? 0;
            int maxFloor = filterRoomDto.MaxFloor ?? 0;
            if (minFloor > 0 || maxFloor > 0)
            {
                var filteredMinFloors = await _db.Rooms.Where(r => r.Floor >= minFloor).ToListAsync();
                var filteredMaxFloors = await _db.Rooms.Where(r => r.Floor <= maxFloor).ToListAsync();

                if (minFloor > 0 && maxId == 0)
                {
                    result.AddRange(filteredMinFloors);
                }
                else if (maxFloor > 0 && minFloor == 0)
                {
                    result.AddRange(filteredMaxFloors);
                }

                result.AddRange(filteredMaxFloors.Intersect(filteredMinFloors));
                filters += 1;
            }

            decimal minPricePerDay = filterRoomDto.MinPricePerDay ?? 0;
            decimal maxPricePerDay = filterRoomDto.MaxPricePerDay ?? 0;
            if (minPricePerDay > 0 || maxPricePerDay > 0)
            {
                var filteredMinPricesPerDay = await _db.Rooms.Where(r => r.PricePerDay >= minPricePerDay).ToListAsync();
                var filteredMaxPricesPerDay = await _db.Rooms.Where(r => r.PricePerDay <= maxPricePerDay).ToListAsync();

                if (minPricePerDay > 0 && maxPricePerDay == 0)
                {
                    result.AddRange(filteredMinPricesPerDay);
                }
                else if (maxPricePerDay > 0 && minPricePerDay == 0)
                {
                    result.AddRange(filteredMaxPricesPerDay);
                }

                result.AddRange(filteredMaxPricesPerDay.Intersect(filteredMinPricesPerDay));
                filters += 1;
            }

            if (!string.IsNullOrEmpty(filterRoomDto.Client))
            {
                var filteredClients = await _db.Rooms.Where(r => EF.Functions.Like(r.Client.FirstName, $"%{filterRoomDto.Client}%")
                                                            || EF.Functions.Like(r.Client.LastName, $"%{filterRoomDto.Client}%")).ToListAsync();
                result.AddRange(filteredClients);
                filters += 1;
            }

            var buildingId = filterRoomDto.BuildingId ?? 0;
            if (buildingId > 0)
            {
                var filteredBuildings = await _db.Rooms.Where(r => r.BuildingId == buildingId).ToListAsync();
                result.AddRange(filteredBuildings);
                filters += 1;
            }

            if (filterRoomDto.IsAvailable)
            {
                var availableRooms = await _db.Rooms.Where(r => r.IsAvailable).ToListAsync();
                result.AddRange(availableRooms);
                if (!availableFiltered) { availableFiltered = true; }
            }
            if (filterRoomDto.IsNotAvailable)
            {
                var notAvailableRooms = await _db.Rooms.Where(r => !r.IsAvailable).ToListAsync();
                result.AddRange(notAvailableRooms);
                if (!availableFiltered) { availableFiltered = true; }
            }

            if (filterRoomDto.IsDamaged)
            {
                var damagedRooms = await _db.Rooms.Where(r => r.IsDamaged).ToListAsync();
                result.AddRange(damagedRooms);
                if (!damagedFiltered) { damagedFiltered = true; }
            }
            if (filterRoomDto.IsNotDamaged)
            {
                var notDamagedRooms = await _db.Rooms.Where(r => !r.IsDamaged).ToListAsync();
                result.AddRange(notDamagedRooms);
                if (!damagedFiltered) { damagedFiltered = true; }
            }

            if (filterRoomDto.IsClean)
            {
                var cleanRooms = await _db.Rooms.Where(r => r.IsClean).ToListAsync();
                result.AddRange(cleanRooms);
                if (!cleanFiltered) { cleanFiltered = true; }
            }
            if (filterRoomDto.IsNotClean)
            {
                var notCleanRooms = await _db.Rooms.Where(r => !r.IsClean).ToListAsync();
                result.AddRange(notCleanRooms);
                if (!cleanFiltered) { cleanFiltered = true; }
            }

            if (filterRoomDto.IsRent)
            {
                var rentRooms = await _db.Rooms.Where(r => r.IsRent).ToListAsync();
                result.AddRange(rentRooms);
                if (!rentFiltered) { rentFiltered = true; }
            }
            if (filterRoomDto.IsNotRent)
            {
                var notRentRooms = await _db.Rooms.Where(r => !r.IsRent).ToListAsync();
                result.AddRange(notRentRooms);
                if (!rentFiltered) { rentFiltered = true; }
            }

            if (availableFiltered) { filters += 1; }
            if (damagedFiltered) { filters += 1; }
            if (cleanFiltered) { filters += 1; }
            if (rentFiltered) { filters += 1; }
            var groupedResults = result.GroupBy(r => r);
            List<Room> finalResult = new List<Room>();
            foreach(var gr in groupedResults)
            {
                if (gr.Count() == filters) { finalResult.Add(gr.Key); }
            }

            return finalResult;
        }

        public async Task<Room> GetAsync(int id)
        {
            var roomGet = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (roomGet != null)
            {
                if (!roomGet.IsDeleted)
                {
                    return roomGet;
                }
                throw new KeyNotFoundException("Requested room is deleted");
            }
            throw new NullReferenceException("Requested room does not exist");
        }

        public async Task<IEnumerable<Room>> ListAsync()
        {
            var rooms = await _db.Rooms.ToListAsync();
            return rooms.Where(r => !r.IsDeleted);
        }

        public async Task<int> UpdateAsync(int id, Room room)
        {
            var roomToUpdate = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (roomToUpdate != null)
            {
                if (!roomToUpdate.IsDeleted)
                {
                    foreach (PropertyInfo prop in room.GetType().GetProperties())
                    {
                        var value = prop.GetValue(room);
                        if (value != null && prop.Name.ToLower() != "id")
                        {
                            prop.SetValue(roomToUpdate, value);
                        }
                    }
                    _db.Rooms.Update(roomToUpdate);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Requested room is deleted");
            }
            throw new NullReferenceException("Requested room does not exist");
        }
    }
}
