using iReception.Models.Dtos.AddDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IRoomToServiceRepository
    {
        Task<int[]> AssignAsync(int id, int[] serviceIds);
        Task<int> DeleteAsync(int roomId);
        Task<int[]> ListAssignedAsync(int roomId);        
    }
}
