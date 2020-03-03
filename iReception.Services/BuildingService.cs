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
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingConverter _converter;
        private readonly IBuildingRepository _repository;

        public BuildingService(IBuildingRepository repository,
                                IBuildingConverter converter)
        {
            _converter = converter;
            _repository = repository;
        }


        public async Task<int> AddBuildingAsync(AddBuildingDto addBuildingDto)
        {
            return await _repository.AddAsync(_converter.AddBuildingDtoToBuilding(addBuildingDto));
        }

        public async Task<int> DeleteBuildingAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<GetBuildingDto>> FilterBuildingsAsync(FilterBuildingDto filterBuildingDto)
        {
            var filteredBuildings = await _repository.FilterAsync(filterBuildingDto);
            return filteredBuildings.Select(c => _converter.BuildingToGetBuildingDto(c)).ToList();
        }

        public async Task<GetBuildingDto> GetBuildingDto(int id)
        {
            var building = await _repository.GetAsync(id);
            return _converter.BuildingToGetBuildingDto(building);
        }

        public async Task<List<GetBuildingDto>> ListBuildingsAsync()
        {
            var buildings = await _repository.ListAsync();
            return buildings.Select(b => _converter.BuildingToGetBuildingDto(b)).ToList();
        }

        public async Task<int> UpdateBuildingAsync(int id, SetBuildingDto setBuildingDto)
        {
            return await _repository.UpdateAsync(id, _converter.SetBuildingDtoToBuilding(setBuildingDto));
        }
    }
}
