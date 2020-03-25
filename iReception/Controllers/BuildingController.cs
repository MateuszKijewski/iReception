using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iReception.Models.Dtos.AddDtos;
using iReception.Models.Dtos.FilterDtos;
using iReception.Models.Dtos.SetDtos;
using iReception.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iReception.App.Controllers
{
    public class BuildingController : Controller
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var buildings = await _buildingService.ListBuildingsAsync();
            ViewBag.Buildings = buildings;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> List(FilterBuildingDto filterBuildingDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var buildings = await _buildingService.FilterBuildingsAsync(filterBuildingDto);
            ViewBag.Buildings = buildings;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBuildingDto addBuildingDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _buildingService.AddBuildingAsync(addBuildingDto);

            return RedirectToAction("list", "building");
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var building = await _buildingService.GetBuildingAsync(id);
                return View(building);
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
                var building = await _buildingService.GetBuildingAsync(id);
                ViewBag.Building = building;
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
        public async Task<IActionResult> Edit(int id, SetBuildingDto setBuildingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _buildingService.UpdateBuildingAsync(id, setBuildingDto);
                return RedirectToAction("show", "building", new { id = id });
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
                await _buildingService.DeleteBuildingAsync(id);
                return RedirectToAction("list", "building");
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
