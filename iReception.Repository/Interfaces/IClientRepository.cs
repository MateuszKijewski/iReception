using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task<int> AddAsync(Client client);
        Task<Client> GetAsync(int? id);
        Task<IEnumerable<Client>> ListAsync();
        Task<int> UpdateAsync(int id, Client client);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Client>> FilterAsync(FilterClientDto filterClientDto);
    }
}
