using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IRoomService
    {
        Task<int> AddRoomAsync(AddRoomDto addRoomDto);
        Task<int> DeleteRoomAsync(int id);
        Task<List<GetRoomDto>> FilterRoomsAsync(FilterRoomDto filterRoomDto);
        Task<GetRoomDto> GetRoomAsync(int id);
        Task<List<GetRoomDto>> ListRoomsAsync();
        Task<int> UpdateRoomAsync(int id, SetRoomDto setRoomDto);

        Task<int[]> AssignServicesAsync(int roomId, int[] serviceIds);
        Task<IEnumerable<GetServiceDto>> ListAssignedServicesAsync(int roomId);
    }
}
