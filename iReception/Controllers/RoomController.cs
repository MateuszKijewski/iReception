using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Repository.Interfaces;
using iReception.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iReception.App.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBuildingService _buildingService;
        private readonly IServiceService _serviceService;
        private readonly IMinuteServiceService _minuteServiceService;

        public RoomController(IRoomService roomService,
                            IBuildingService buildingService,
                            IServiceService serviceService,
                            IMinuteServiceService minuteServiceService)
        {
            _roomService = roomService;
            _buildingService = buildingService;
            _serviceService = serviceService;
            _minuteServiceService = minuteServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var rooms = await _roomService.ListRoomsAsync();
            ViewBag.Rooms = rooms;
            var buildings = await _buildingService.ListBuildingsAsync();
            var model = new FilterRoomDto();
            model.Buildings = buildings;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> List(FilterRoomDto filterRoomDto)
        {
            var buildings = await _buildingService.ListBuildingsAsync();
            var model = new FilterRoomDto();
            model.Buildings = buildings;

            if (!ModelState.IsValid)
            {                                
                return View(model);
            }
            var rooms = await _roomService.FilterRoomsAsync(filterRoomDto);
            ViewBag.Rooms = rooms;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> listOffer()
        {
            var rooms = await _roomService.ListRoomsAsync();
            ViewBag.Rooms = rooms;
            var buildings = await _buildingService.ListBuildingsAsync();
            var model = new FilterRoomDto();
            model.Buildings = buildings;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> listOffer(FilterRoomDto filterRoomDto)
        {
            var buildings = await _buildingService.ListBuildingsAsync();
            var model = new FilterRoomDto();
            model.Buildings = buildings;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var rooms = await _roomService.FilterRoomsAsync(filterRoomDto);
            ViewBag.Rooms = rooms;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var buildings = await _buildingService.ListBuildingsAsync();
            var model = new AddRoomDto();
            model.Buildings = buildings;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRoomDto addRoomDto)
        {
            if (!ModelState.IsValid)
            {
                var buildings = await _buildingService.ListBuildingsAsync();
                var model = new AddRoomDto();
                model.Buildings = buildings;
                return View(model);
            }
            await _roomService.AddRoomAsync(addRoomDto);
            var rooms = await _roomService.ListRoomsAsync();
            int roomId = rooms.OrderByDescending(r => r.Id).FirstOrDefault().Id;

            return RedirectToAction("show", "room", new { id = roomId });
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var assignedServices = await _roomService.ListAssignedServicesAsync(id);
                var assignedMinuteServices = await _roomService.ListAssignedMinuteServicesAsync(id);

                ViewBag.Services = assignedServices;
                ViewBag.MinuteServices = assignedMinuteServices;

                var room = await _roomService.GetRoomAsync(id);                
                return View(room);
            }
            catch (Exception e)
            {
                if (e is FormatException
                    || e is NullReferenceException
                    || e is KeyNotFoundException)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var services = await _serviceService.ListServiceAsync();
                var minuteServices = await _minuteServiceService.ListMinuteServicesAsync();                               
                var assignedServicesIds = 
                    (await _roomService.ListAssignedServicesAsync(id))
                    .Select(s => s.Id)
                    .ToArray();
                var assignedMinuteServicesids =
                    (await _roomService.ListAssignedMinuteServicesAsync(id))
                    .Select(s => s.Id)
                    .ToArray();

                var serviceSelect = new MultiSelectList(services, "Id", "Name");
                var minuteServiceSelect = new MultiSelectList(minuteServices, "Id", "Name");

                foreach(var option in serviceSelect)
                {
                    int? selectedOptionId = assignedServicesIds.FirstOrDefault(asi => asi.ToString() == option.Value);
                    if (selectedOptionId != 0)
                    {
                        option.Selected = true;
                    }                    
                }
                foreach (var option in minuteServiceSelect)
                {
                    int? selectedOptionId = assignedMinuteServicesids.FirstOrDefault(asi => asi.ToString() == option.Value);
                    if (selectedOptionId != 0)
                    {
                        option.Selected = true;
                    }
                }

                ViewBag.Services = serviceSelect;
                ViewBag.MinuteServices = minuteServiceSelect;


                var buildings = await _buildingService.ListBuildingsAsync();
                var model = new SetRoomDto();
                model.Buildings = buildings;

                var room = await _roomService.GetRoomAsync(id);
                ViewBag.Room = room;
                return View(model);
            }
            catch (Exception e)
            {
                if (e is FormatException
                    || e is NullReferenceException
                    || e is KeyNotFoundException)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SetRoomDto setRoomDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var buildings = await _buildingService.ListBuildingsAsync();
                    var model = new SetRoomDto();
                    model.Buildings = buildings;

                    var room = await _roomService.GetRoomAsync(id);
                    ViewBag.Room = room;
                    return View(model);
                }
                await _roomService.UpdateRoomAsync(id, setRoomDto);
                return RedirectToAction("show", "room", new { id = id });
            }
            catch (Exception e)
            {
                if (e is FormatException
                    || e is NullReferenceException
                    || e is KeyNotFoundException)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                return RedirectToAction("list", "room");
            }
            catch (Exception e)
            {
                if (e is FormatException
                    || e is NullReferenceException
                    || e is KeyNotFoundException)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignServices(RoomServicesDto roomServicesDto)
        {           
            await _roomService.AssignServicesAsync(roomServicesDto.RoomId, roomServicesDto.AssignedIds);
            return RedirectToAction("edit", "room", new { id = roomServicesDto.RoomId });
        }

        [HttpPost]
        public async Task<IActionResult> AssignMinuteServices(RoomServicesDto roomServicesDto)
        {
            await _roomService.AssignMinuteServicesAsync(roomServicesDto.RoomId, roomServicesDto.AssignedIds);
            return RedirectToAction("edit", "room", new {id = roomServicesDto.RoomId});
        }

    }
}
