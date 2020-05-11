using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;

namespace iReception.Services.Interfaces
{
    public interface IReservationService
    {
        Task<int> AddReservationAsync(AddReservationDto addReservationDto);
        Task<GetReservationDto> GetReservationAsync(int id);
        Task<int> DeleteReservationAsync(int id);
        Task<IEnumerable<GetReservationDto>> FilterReservationsAsync(FilterReservationDto filterReservationDto);
        Task<IEnumerable<GetReservationDto>> ListReservationsAsync();
        Task<int> PayForReservationAsync(int id);
        Task<GetInvoiceDto> GetInvoiceData(int id);
    }
}
