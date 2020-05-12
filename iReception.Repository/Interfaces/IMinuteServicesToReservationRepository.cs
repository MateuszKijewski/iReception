using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Entities;

namespace iReception.Repository.Interfaces
{
    public interface IMinuteServicesToReservationRepository
    {
        Task<int> AddAsync(MinuteServiceToReservation minuteServiceToReservation);
        Task<IEnumerable<MinuteServiceToReservation>> ListAssignedAsync(int reservationId);
        Task<int> DeleteAsync(int reservationId, int minuteServiceId);
    }
}
