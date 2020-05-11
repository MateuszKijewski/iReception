using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iReception.Models.Converters.Interfaces;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.GetDtos;
using iReception.Repository.Interfaces;
using iReception.Services.Interfaces;

namespace iReception.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationConverter _reservationConverter;
        private readonly IHotelCompanyRepository _hotelCompanyRepository;
        private readonly IHotelCompanyConverter _hotelCompanyConverter;
        private readonly IInvoicePositionConverter _invoicePositionConverter;

        //converterHelpers
        private readonly IClientRepository _clientRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMinuteServicesToReservationRepository _minuteServicesToReservationRepository;

        public ReservationService(IReservationRepository reservationRepository,
                                IReservationConverter reservationConverter,
                                IHotelCompanyRepository hotelCompanyRepository,
                                IHotelCompanyConverter hotelCompanyConverter,
                                IInvoicePositionConverter invoicePositionConverter,
                                IClientRepository clientRepository,
                                IRoomRepository roomRepository,
                                IMinuteServicesToReservationRepository minuteServicesToReservationRepository)
        {
            _reservationRepository = reservationRepository;
            _reservationConverter = reservationConverter;
            _hotelCompanyRepository = hotelCompanyRepository;
            _hotelCompanyConverter = hotelCompanyConverter;
            _invoicePositionConverter = invoicePositionConverter;
            _clientRepository = clientRepository;
            _roomRepository = roomRepository;
            _minuteServicesToReservationRepository = minuteServicesToReservationRepository;
        }

        public async Task<int> AddReservationAsync(AddReservationDto addReservationDto)
        {
            return await _reservationRepository.AddAsync(
                _reservationConverter.AddReservationDtoToReservation(addReservationDto));
        }

        public async Task<GetReservationDto> GetReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetAsync(id);
            reservation.Client = await _clientRepository.GetAsync(reservation.ClientId);
            reservation.Room = await _roomRepository.GetAsync(reservation.RoomId);

            return _reservationConverter.ReservationToGetReservationDto(reservation);
        }

        public async Task<int> DeleteReservationAsync(int id)
        {
            return await _reservationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GetReservationDto>> FilterReservationsAsync(FilterReservationDto filterReservationDto)
        {
            var filteredReservations = await _reservationRepository.FilterAsync(filterReservationDto);
            var reservations = filteredReservations.ToList();
            foreach (var reservation in reservations)
            {
                reservation.Client = await _clientRepository.GetAsync(reservation.ClientId);
                reservation.Room = await _roomRepository.GetAsync(reservation.RoomId);
            }

            return reservations.Select(r => _reservationConverter.ReservationToGetReservationDto(r));
        }

        public async Task<IEnumerable<GetReservationDto>> ListReservationsAsync()
        {
            var listedReservations = await _reservationRepository.ListAsync();
            var reservations = listedReservations.ToList();
            foreach (var reservation in reservations)
            {
                reservation.Client = await _clientRepository.GetAsync(reservation.ClientId);
                reservation.Room = await _roomRepository.GetAsync(reservation.RoomId);
            }


            return reservations.Select(r => _reservationConverter.ReservationToGetReservationDto(r));
        }

        public async Task<int> PayForReservationAsync(int id)
        {
            return await _reservationRepository.PayAsync(id);
        }

        public async Task<GetInvoiceDto> GetInvoiceData(int id)
        {
            var invoiceData = new GetInvoiceDto();

            var reservation = await _reservationRepository.GetAsync(id);
            reservation.Client = await _clientRepository.GetAsync(reservation.ClientId);
            reservation.Room = await _roomRepository.GetAsync(reservation.RoomId);

            // Id usług przypisanych do 
            var reservationMinuteServicesIds = await _minuteServicesToReservationRepository.ListAssignedAsync(id);
            

            var reservationPosition = _invoicePositionConverter.ReservationToGetInvoicePositionDto(reservation);
            

            invoiceData.IssueDate = DateTime.Today;
            invoiceData.PaymentDate = DateTime.Today;

            return invoiceData;
        }
    }
}
