using iReception.Models.Dtos;
using iReception.Models.Dtos.AddDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iReception.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(RegisterUserDto registerUserDto, List<String> errors);
        Task Logout();
        Task Login(LoginUserDto loginUserDto, List<String> errors);
        Task<bool> CheckMail(string email);
    }
}
