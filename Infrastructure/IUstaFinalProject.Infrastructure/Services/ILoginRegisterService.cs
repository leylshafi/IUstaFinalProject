﻿using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Domain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Infrastructure.Services
{
    public interface ILoginRegisterService
    {
        Task<bool> Register(RegisterDto request,Role role,string? catId);
        Task<string> Login(UserDto request,Role role);
    }
}
