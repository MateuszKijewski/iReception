using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;

namespace iReception.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly iReceptionDbContext _db;

        public ReservationRepository(iReceptionDbContext db)
        {
            _db = db;
        }


        public async Task<int> AddAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reservation>> FilterAsync(FilterReservationDto filterReservationDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reservation>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> PayAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetInvoiceDataAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
