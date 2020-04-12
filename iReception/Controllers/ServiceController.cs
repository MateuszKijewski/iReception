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
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var services = await _serviceService.ListServiceAsync();
            ViewBag.Services = services;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> List(FilterServiceDto filterServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var services = await _serviceService.FilterServicesAsync(filterServiceDto);
            ViewBag.Services = services;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddServiceDto addServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _serviceService.AddServiceAsync(addServiceDto);

            return RedirectToAction("list", "service");
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var service = await _serviceService.GetServiceAsync(id);
                return View(service);
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
                var service = await _serviceService.GetServiceAsync(id);
                ViewBag.Service = service;
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
        public async Task<IActionResult> Edit(int id, SetServiceDto setServiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _serviceService.UpdateServiceAsync(id, setServiceDto);
                return RedirectToAction("show", "service", new { id = id });
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
                await _serviceService.DeleteServiceAsync(id);
                return RedirectToAction("list", "service");
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
