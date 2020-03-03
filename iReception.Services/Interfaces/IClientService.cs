using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Models.Dtos.SetDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Services.Interfaces
{
    public interface IClientService
    {
        Task<int> AddClientAsync(AddClientDto addClientDto);
        Task<GetClientDto> GetClientAsync(int id);
        Task<List<GetClientDto>> ListClientsAsync();
        Task<int> UpdateClientAsync(int id, SetClientDto setClientDto);
        Task<int> DeleteClientAsync(int id);
        Task<List<GetClientDto>> FilterClientsAsync(FilterClientDto filterClientDto);
    }
}
