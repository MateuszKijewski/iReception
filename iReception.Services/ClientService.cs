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
    public class ClientService : IClientService
    {
        private readonly IClientConverter _converter;
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository,
                             IClientConverter converter)
        {
            _converter = converter;
            _repository = repository;
        }

        public async Task<int> AddClientAsync(AddClientDto addClientDto)
        {
            return await _repository.AddAsync(_converter.AddClientDtoToClient(addClientDto));
        }

        public async Task<GetClientDto> GetClientAsync(int id)
        {
            var client = await _repository.GetAsync(id);
            return _converter.ClientToGetClientDto(client);
        }

        public async Task<List<GetClientDto>> ListClientsAsync()
        {
            var clients = await _repository.ListAsync();
            return clients.Select(c => _converter.ClientToGetClientDto(c)).ToList();
            
        }

        public async Task<int> UpdateClientAsync(int id, SetClientDto setClientDto)
        {
            return await _repository.UpdateAsync(id, _converter.SetClientDtoToClient(setClientDto));
        }

        public async Task<int> DeleteClientAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<GetClientDto>> FilterClientsAsync(FilterClientDto filterClientDto)
        {
            var filteredClients = await _repository.FilterAsync(filterClientDto);
            return filteredClients.Select(c => _converter.ClientToGetClientDto(c)).ToList();
        }
    }
}
