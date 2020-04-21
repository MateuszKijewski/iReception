using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IRoomToMinuteServiceRepository
    {
        Task<int[]> AssignAsync(int roomId, int[] serviceIds);
        Task<int> DeleteAsync(int roomId);
        Task<int[]> ListAssignedAsync(int roomId);
    }
}
