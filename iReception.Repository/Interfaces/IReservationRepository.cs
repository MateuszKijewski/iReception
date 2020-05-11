using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Entities;

namespace iReception.Repository.Interfaces
{
    public interface IReservationRepository
    {
        Task<int> AddAsync(Reservation reservation);
        Task<Reservation> GetAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Reservation>> FilterAsync(FilterReservationDto filterReservationDto);
        Task<IEnumerable<Reservation>> ListAsync();

        Task<int> PayAsync(int id);
    }
}
