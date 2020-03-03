using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
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
            await _db.Clients.AddAsync(client);
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
            return clients.Where(c => !c.IsDeleted);
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

        public async Task<IEnumerable<Client>> FilterAsync(FilterClientDto filterClientDto)
        {
            
            int minId = filterClientDto.MinId ?? 0;
            int maxId = filterClientDto.MaxId ?? 0;
            int filters = 0;

            List<Client> result = new List<Client>();
            if (minId > 0 || maxId > 0)
            {
                var filteredMinIds = await _db.Clients.Where(c => c.Id >= minId).ToListAsync();
                var filteredMaxIds = await _db.Clients.Where(c => c.Id <= maxId).ToListAsync();

                if (minId > 0 && maxId == 0)
                {
                    result.AddRange(filteredMinIds);
                }
                else if (maxId > 0 && minId == 0)
                {
                    result.AddRange(filteredMaxIds);
                }

                result.AddRange(filteredMaxIds.Intersect(filteredMinIds));
                filters += 1;
            }
            

            if (!string.IsNullOrEmpty(filterClientDto.FirstName))
            {
                var filteredFirstNames = await _db.Clients.Where(c => EF.Functions.Like(c.FirstName, $"%{filterClientDto.FirstName}%")).ToListAsync();
                result.AddRange(filteredFirstNames);
                filters += 1;
            }
            if (!string.IsNullOrEmpty(filterClientDto.LastName))
            {
                var filteredLastNames = await _db.Clients.Where(c => EF.Functions.Like(c.LastName, $"%{filterClientDto.LastName}%")).ToListAsync();
                result.AddRange(filteredLastNames);
                filters += 1;
            }
            if (!string.IsNullOrEmpty(filterClientDto.PhoneNumber))
            {
                var filteredPhoneNumbers = await _db.Clients.Where(c => EF.Functions.Like(c.PhoneNumber, $"%{filterClientDto.PhoneNumber}%")).ToListAsync();
                result.AddRange(filteredPhoneNumbers);
                filters += 1;
            }
            if (!string.IsNullOrEmpty(filterClientDto.Email))
            {
                var filteredEmails= await _db.Clients.Where(c => EF.Functions.Like(c.Email, $"%{filterClientDto.Email}%")).ToListAsync();
                result.AddRange(filteredEmails);
                filters += 1;
            }
            if (!string.IsNullOrEmpty(filterClientDto.PaymentMethod))
            {
                var filteredPaymentMethods = await _db.Clients.Where(c => EF.Functions.Like(c.PaymentMethod, $"%{filterClientDto.PaymentMethod}%")).ToListAsync();
                result.AddRange(filteredPaymentMethods);
                filters += 1;
            }
            if (!string.IsNullOrEmpty(filterClientDto.CreditCard))
            {
                var filteredCreditCards= await _db.Clients.Where(c => EF.Functions.Like(c.CreditCard, $"%{filterClientDto.CreditCard}%")).ToListAsync();
                result.AddRange(filteredCreditCards);
                filters += 1;
            }
            if (!string.IsNullOrEmpty(filterClientDto.IdDocumentNumber))
            {
                var filteredDocumentNumbers= await _db.Clients.Where(c => EF.Functions.Like(c.IdDocumentNumber, $"%{filterClientDto.IdDocumentNumber}%")).ToListAsync();
                result.AddRange(filteredDocumentNumbers);
                filters += 1;
            }

            var groupedResults = result.GroupBy(r => r);
            List<Client> finalResult = new List<Client>();
            foreach (var gr in groupedResults)
            {
                if (gr.Count() == filters) { finalResult.Add(gr.Key); }
            }

            return finalResult;

        }
    }
}
