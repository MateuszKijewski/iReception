using iReception.Models.Dtos.AddDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Repository.Interfaces
{
    public interface IRoomToServiceRepository
    {
        Task<IEnumerable<int>> AssignAsync(IEnumerable<AssignServiceDto> assignServiceDtos);
        Task<int> AssignAsync(int roomId);
        Task<int[]> ListAssignedAsync(int roomId);        
    }
}
