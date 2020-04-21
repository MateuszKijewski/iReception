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
using Microsoft.Extensions.WebEncoders.Testing;

namespace iReception.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomConverter _converter;
        private readonly IRoomRepository _repository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IRoomToServiceRepository _roomToServiceRepository;
        private readonly IRoomToMinuteServiceRepository _roomToMinuteServiceRepository;
        private readonly IServiceService _serviceService;
        private readonly IMinuteServiceService _minuteServiceService;

        public RoomService(IRoomConverter converter, 
                        IRoomRepository repository,
                        IBuildingRepository buildingRepository,
                        IRoomToServiceRepository roomToServiceRepository,
                        IRoomToMinuteServiceRepository roomToMinuteServiceRepository,
                        IServiceService serviceService, 
                        IMinuteServiceService minuteServiceService)
        {
            _converter = converter;
            _repository = repository;
            _buildingRepository = buildingRepository;
            _roomToServiceRepository = roomToServiceRepository;
            _roomToMinuteServiceRepository = roomToMinuteServiceRepository;
            _serviceService = serviceService;
            _minuteServiceService = minuteServiceService;
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

        /* Services methods */

        public async Task<int[]> AssignServicesAsync(int roomId, int[] serviceIds)
        {
            //var serviceAssignments = new List<AssignServiceDto>();
            if (serviceIds != null)
            {
                return await _roomToServiceRepository.AssignAsync(roomId, serviceIds);
            }

            return new[]{ await _roomToServiceRepository.DeleteAsync(roomId) };

        }

        public async Task<IEnumerable<GetServiceDto>> ListAssignedServicesAsync(int roomId)
        {
            var assignedServicesIds = await _roomToServiceRepository.ListAssignedAsync(roomId);
            List<GetServiceDto> assignedServices = new List<GetServiceDto>();

            foreach(var id in assignedServicesIds)
            {
                assignedServices.Add(await _serviceService.GetServiceAsync(id));
            }
            return assignedServices;
        }

        public async Task<int[]> AssignMinuteServicesAsync(int roomId, int[] minuteServiceIds)
        {
            if (minuteServiceIds != null)
            {
                return await _roomToMinuteServiceRepository.AssignAsync(roomId, minuteServiceIds);
            }

            return new[] {await _roomToMinuteServiceRepository.DeleteAsync(roomId)};
        }

        public async Task<IEnumerable<GetMinuteServiceDto>> ListAssignedMinuteServicesAsync(int roomId)
        {
            var assignedMinuteServicesIds = await _roomToMinuteServiceRepository.ListAssignedAsync(roomId);
            List<GetMinuteServiceDto> assignedMinuteServices = new List<GetMinuteServiceDto>();

            foreach (var id in assignedMinuteServicesIds)
            {
                assignedMinuteServices.Add(await _minuteServiceService.GetMinuteServiceAsync(id));
            }

            return assignedMinuteServices;
        }
    }
}
