using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Entities;

namespace iReception.Repository.Interfaces
{
    public interface IMinuteServicesToReservationRepository
    {
        Task<int[]> AddAsync(int reservationId, Dictionary<int, int> servicesWithDuration);
        Task<int[]> ListAssignedAsync();
        Task<int> DeleteAsync(int reservationId, int minuteServiceId);
    }
}
