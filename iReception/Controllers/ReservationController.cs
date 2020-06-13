using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iReception.Models.Dtos.AddDtos;
using iReception.Repository.Interfaces;
using iReception.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iReception.App.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMinuteServiceService _minuteServiceService;
        private readonly IReservationService _reservationService;

        public ReservationController(IRoomService roomService,
                                    IMinuteServiceService minuteServiceService,
                                    IReservationService reservationService)
        {
            _roomService = roomService;
            _minuteServiceService = minuteServiceService;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> BookRoom(int roomId)
        {
            var room = await _roomService.GetRoomAsync(roomId);
            var assignedServices = await _roomService.ListAssignedServicesAsync(roomId);
            var assignedMinuteServices = await _roomService.ListAssignedMinuteServicesAsync(roomId);

            var bookServicesDictionary = new Dictionary<int, int>();
            foreach (var minuteService in assignedMinuteServices)
            {
                bookServicesDictionary.Add(minuteService.Id, 0);
            }
            ViewBag.Services = assignedServices;
            ViewBag.BookServices = bookServicesDictionary;
            ViewBag.MinuteServices = assignedMinuteServices;

            ViewBag.Room = room;
            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BookRoom(AddReservationDto addReservationDto)
        {
            if (!ModelState.IsValid)
            {
                foreach(var model in ModelState.Values)
                {
                    foreach (var error in model.Errors) 
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return RedirectToAction("BookRoom", "Reservation", new { roomId = addReservationDto.RoomId });
            }
            var reservationId = await _reservationService.AddReservationAsync(addReservationDto);
            foreach(var bookedService in addReservationDto.MinuteServices)
            {
                await _reservationService.AssignMinuteServiceAsync(new AddMinuteServiceToReservationDto() { 
                    ReservationId = reservationId,
                    MinuteServiceId = bookedService.MinuteServiceId,
                    Duration = bookedService.Duration
                });
            }

            return RedirectToAction("Manage", "Reservation", new { reservationId = reservationId });
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int reservationId) 
        {
            var reservation = await _reservationService.GetReservationAsync(reservationId);

            return View(reservation);
        }
    }
}
