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
    public class BuildingConverter : IBuildingConverter
    {
        public Building AddBuildingDtoToBuilding(AddBuildingDto addBuildingDto)
        {
            return new Building
            {
                Address = addBuildingDto.Address,
                ShortName = addBuildingDto.ShortName
            };
        }

        public GetBuildingDto BuildingToGetBuildingDto(Building building)
        {
            return new GetBuildingDto
            {
                Id = building.Id,
                Address = building.Address,
                ShortName = building.ShortName,
                IsAvailable = building.IsAvailable,
                Rooms = building.Rooms
                
            };
        }

        public Building SetBuildingDtoToBuilding(SetBuildingDto setBuildingDto)
        {
            return new Building
            {
                Address = setBuildingDto.Address,
                ShortName = setBuildingDto.ShortName,
                IsAvailable = setBuildingDto.IsAvailable
            };
        }
    }
}
