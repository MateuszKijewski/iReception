using iReception.Models.Dtos;
using iReception.Models.Dtos.AddDtos;
using iReception.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task Login(LoginUserDto loginUserDto, List<String> errors)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email,
                                                                  loginUserDto.Password,
                                                                  loginUserDto.RememberMe,
                                                                  false);
            if (!result.Succeeded)
            {
                errors.Add("Invalid login attempt");
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterUserDto registerUserDto, List<String> errors)
        {
            var user = new IdentityUser
            {
                UserName = registerUserDto.Email,
                Email = registerUserDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);                
            }

            foreach (var exception in result.Errors)
            {
                errors.Add(exception.Description);
            }
    
        }
    }
}
