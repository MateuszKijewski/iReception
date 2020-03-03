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
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {

            var clients = await _clientService.ListClientsAsync();
            ViewBag.Clients = clients;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> List(FilterClientDto filterClientDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var clients = await _clientService.FilterClientsAsync(filterClientDto);
            ViewBag.Clients = clients;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddClientDto addClientDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _clientService.AddClientAsync(addClientDto);

            return RedirectToAction("list", "client");
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var client = await _clientService.GetClientAsync(id);
                return View(client);
            }
            catch(Exception e)
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
                var client = await _clientService.GetClientAsync(id);
                ViewBag.Client = client;
                return View();
            }
            catch(Exception e)
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
        public async Task<IActionResult> Edit(int id, SetClientDto setClientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _clientService.UpdateClientAsync(id, setClientDto);
                return RedirectToAction("show", "client", new { id = id });
            }
            catch(Exception e)
            {
                if(e is FormatException
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
                await _clientService.DeleteClientAsync(id);
                return RedirectToAction("list", "client");
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
