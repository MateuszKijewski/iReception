using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Repository.Interfaces;
using iReception.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomConverter _converter;
        private readonly IRoomRepository _repository;
        private readonly IBuildingRepository _buildingRepository;

        public RoomService(IRoomConverter converter, 
                        IRoomRepository repository,
                        IBuildingRepository buildingRepository)
        {
            _converter = converter;
            _repository = repository;
            _buildingRepository = buildingRepository;
        }

        public async Task<int> AddRoomAsync(AddRoomDto addRoomDto)
        {
            return await _repository.AddAsync(_converter.AddRoomDtoToRoom(addRoomDto)); 
        }

        public async Task<int> DeleteRoomAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<GetRoomDto>> FilterRoomsAsync(FilterRoomDto filterRoomDto)
        {
            var filteredRooms = await _repository.FilterAsync(filterRoomDto);
            return filteredRooms.Select(r => _converter.RoomToGetRoomDto(r)).ToList();
        }

        public async Task<GetRoomDto> GetRoomAsync(int id)
        {
            var room = await _repository.GetAsync(id);
            room.Building = await _buildingRepository.GetAsync(room.BuildingId);
            return _converter.RoomToGetRoomDto(room);
        }

        public async Task<List<GetRoomDto>> ListRoomsAsync()
        {
            var rooms = await _repository.ListAsync();            
            foreach (var r in rooms)
            {
                r.Building = await _buildingRepository.GetAsync(r.BuildingId);
            }
            return rooms.Select(r => _converter.RoomToGetRoomDto(r)).ToList();
        }

        public async Task<int> UpdateRoomAsync(int id, SetRoomDto setRoomDto)
        {
            return await _repository.UpdateAsync(id, _converter.SetRoomDtoToRoom(setRoomDto));
        }

    }
}
