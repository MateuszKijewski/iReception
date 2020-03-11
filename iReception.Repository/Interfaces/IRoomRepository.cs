using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IRoomRepository
    {
        Task<int> AddAsync(Room room);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Room>> FilterAsync(FilterRoomDto filterRoomDto);
        Task<Room> GetAsync(int id);
        Task<IEnumerable<Room>> ListAsync();
        Task<int> UpdateAsync(int id, Room room);
    }
}
