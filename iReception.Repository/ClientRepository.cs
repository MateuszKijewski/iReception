using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace iReception.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly iReceptionDbContext _db;

        public ClientRepository(iReceptionDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Client client)
        {
            client.IsDeleted = false;
            _db.Clients.Add(client);
            await _db.SaveChangesAsync();
            return client.Id;
        }

        public async Task<Client> GetAsync(int id)
        {
            var clientGet = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (clientGet != null)
            {
                if (!clientGet.IsDeleted)
                {
                    return clientGet;
                }
                throw new KeyNotFoundException("Requested client is deleted");
            }
            throw new NullReferenceException("Client not found in database");
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            var clients = await _db.Clients.ToListAsync();
            return clients.Where(c => c.IsDeleted != true);
        }

        public async Task<int> UpdateAsync(int id, Client client)
        {            
            var clientToUpdate = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if(clientToUpdate != null)
            {
                if (!clientToUpdate.IsDeleted)
                {
                    foreach (PropertyInfo prop in client.GetType().GetProperties())
                    {
                        var value = prop.GetValue(client);
                        if (value != null && prop.Name.ToLower() != "id")
                        {
                            prop.SetValue(clientToUpdate, value);
                        }
                    }
                    _db.Clients.Update(clientToUpdate);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Requested client is deleted");
            }            
            throw new NullReferenceException("Requested client does not exist");
        }

        public async Task<int> DeleteAsync(int id)
        {
            var clientToDelete = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (clientToDelete != null)
            {
                if (!clientToDelete.IsDeleted)
                {
                    clientToDelete.IsDeleted = true;
                    _db.Clients.Update(clientToDelete);
                    await _db.SaveChangesAsync();
                    return id;
                }
                throw new KeyNotFoundException("Client is already deleted");
            }
            throw new NullReferenceException("Requested client does not exist");
        }
    }
}
