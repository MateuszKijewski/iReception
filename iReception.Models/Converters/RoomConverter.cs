using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters
{
    public class RoomConverter : IRoomConverter
    {
        private readonly IBuildingConverter _buildingConverter;
        private readonly IClientConverter _clientConverter;

        public RoomConverter(IBuildingConverter buildingConverter, IClientConverter clientConverter)
        {
            _buildingConverter = buildingConverter;
            _clientConverter = clientConverter;
        }

        public Room AddRoomDtoToRoom(AddRoomDto addRoomDto)
        {
            return new Room
            {
                Number = addRoomDto.Number,
                Standard = addRoomDto.Standard,
                Floor = addRoomDto.Floor,
                PricePerDay = addRoomDto.PricePerDay,
                BuildingId = addRoomDto.BuildingId
            };
        }

        public GetRoomDto RoomToGetRoomDto(Room room)
        {
            return new GetRoomDto
            {
                Id = room.Id,
                Number = room.Number,
                Standard = room.Standard,
                Floor = room.Floor,
                PricePerDay = room.PricePerDay,
                Client = _clientConverter.ClientToGetClientDto(room.Client),
                Building = _buildingConverter.BuildingToGetBuildingDto(room.Building),

                IsAvailable = room.IsAvailable,
                IsDamaged = room.IsDamaged,
                IsClean = room.IsClean,
                IsRent = room.IsRent
            };
        }

        public Room SetRoomDtoToRoom(SetRoomDto setRoomDto)
        {
            return new Room
            {
                Number = setRoomDto.Number,
                Standard = setRoomDto.Standard,
                Floor = setRoomDto.Floor,
                PricePerDay = setRoomDto.PricePerDay,
                BuildingId = setRoomDto.BuildingId,
                IsAvailable = setRoomDto.IsAvailable,
                IsDamaged = setRoomDto.IsDamaged,
                IsClean = setRoomDto.IsClean,
                IsRent = setRoomDto.IsRent                
            };
        }
    }
}
