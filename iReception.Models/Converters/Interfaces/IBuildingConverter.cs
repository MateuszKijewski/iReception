using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Converters.Interfaces
{
    public interface IBuildingConverter
    {
        Building AddBuildingDtoToBuilding(AddBuildingDto addBuildingDto);
        GetBuildingDto BuildingToGetBuildingDto(Building building);
        Building SetBuildingDtoToBuilding(SetBuildingDto setBuildingDto);
    }
}
