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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iReception.App.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBuildingService _buildingService;

        public RoomController(IRoomService roomService, IBuildingService buildingService)
        {
            _roomService = roomService;
            _buildingService = buildingService;
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
    }
}
