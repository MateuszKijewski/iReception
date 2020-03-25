using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iReception.Models.Converters
{
    public class RoomConverter : IRoomConverter
    {
        private readonly IBuildingConverter _buildingConverter;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RoomConverter(IBuildingConverter buildingConverter,
                            IHostingEnvironment hostingEnvironment)
        {
            _buildingConverter = buildingConverter;
            _hostingEnvironment = hostingEnvironment;
        }

        string fileName = null;
        public Room AddRoomDtoToRoom(AddRoomDto addRoomDto)
        {
            if (addRoomDto.Photo != null)
            {
                string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                fileName = $"{Guid.NewGuid()}_{addRoomDto.Photo.FileName}";
                string filePath = Path.Combine(uploadPath, fileName);
                addRoomDto.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return new Room
            {
                Number = addRoomDto.Number,
                Standard = addRoomDto.Standard,
                Floor = addRoomDto.Floor,
                PricePerDay = addRoomDto.PricePerDay,
                BuildingId = addRoomDto.BuildingId,
                PhotoPath = fileName                
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
                Building = _buildingConverter.BuildingToGetBuildingDto(room.Building),
                IsAvailable = room.IsAvailable,
                IsDamaged = room.IsDamaged,
                IsClean = room.IsClean,
                IsRent = room.IsRent,
                PhotoPath = room.PhotoPath
            };
        }

        public Room SetRoomDtoToRoom(SetRoomDto setRoomDto)
        {
            if (setRoomDto.Photo != null)
            {
                string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                fileName = $"{Guid.NewGuid()}_{setRoomDto.Photo.FileName}";
                string filePath = Path.Combine(uploadPath, fileName);
                setRoomDto.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
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
                IsRent = setRoomDto.IsRent,
                PhotoPath = fileName
            };
        }
    }
}
