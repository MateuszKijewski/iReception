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

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> List(FilterRoomDto filterRoomDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var rooms = await _roomService.FilterRoomsAsync(filterRoomDto);
            ViewBag.Rooms = rooms;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var buildings = _buildingService.ListBuildingsAsync();
            ViewBag.Buildings = buildings;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRoomDto addRoomDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _roomService.AddRoomAsync(addRoomDto);

            return RedirectToAction("list", "room");
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
                var buildings = _buildingService.ListBuildingsAsync();
                ViewBag.Buildings = buildings;

                var room = await _roomService.GetRoomAsync(id);
                ViewBag.Room = room;
                return View();
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
                    return View();
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
