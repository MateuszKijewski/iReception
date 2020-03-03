using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iReception.Models.Dtos;
using iReception.Models.Dtos.AddDtos;
using iReception.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iReception.App.Controllers
{
    public class AccountController : Controller
    {
 
        private readonly IUserService _userService;


        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var errors = new List<String>();
            await _userService.Register(registerUserDto, errors);

            if (errors.Count == 0)
            {
                return RedirectToAction("index", "home");
            }


            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var errors = new List<String>();
            await _userService.Login(loginUserDto, errors);

            if (errors.Count == 0)
            {
                return RedirectToAction("index", "home");
            }

            foreach(var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(loginUserDto);
        }

        /* Remote validation */
        [HttpGet][HttpPost]
        public async Task<IActionResult> IsEmailUsed(string email)
        {
            var mailExists = await _userService.CheckMail(email);
            return mailExists ? Json($"User with email {email} already exists.") : Json(true);
        }
    }
}
