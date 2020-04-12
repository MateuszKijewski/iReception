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
    public class MinuteServiceController : Controller
    {
        private readonly IMinuteServiceService _minuteServiceService;

        public MinuteServiceController(IMinuteServiceService minuteServiceService)
        {
            _minuteServiceService = minuteServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var minuteServices = await _minuteServiceService.ListMinuteServicesAsync();
            ViewBag.MinuteServices = minuteServices;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> List(FilterMinuteServiceDto filterMinuteServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var minuteServices = await _minuteServiceService.FilterMinuteServicesAsync(filterMinuteServiceDto);
            ViewBag.MinuteServices = minuteServices;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMinuteServiceDto addMinuteServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _minuteServiceService.AddMinuteServiceAsync(addMinuteServiceDto);

            return RedirectToAction("list", "minuteservice");
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var minuteService = await _minuteServiceService.GetMinuteServiceAsync(id);
                return View(minuteService);
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
                var minuteService = await _minuteServiceService.GetMinuteServiceAsync(id);
                ViewBag.MinuteService = minuteService;
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
        public async Task<IActionResult> Edit(int id, SetMinuteServiceDto setMinuteServiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _minuteServiceService.UpdateMinuteServiceAsync(id, setMinuteServiceDto);
                return RedirectToAction("show", "minuteservice", new { id = id });
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
                await _minuteServiceService.DeleteMinuteServiceAsync(id);
                return RedirectToAction("list", "minuteservice");
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
