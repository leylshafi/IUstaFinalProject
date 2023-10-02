using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUstaFinalProject.Persistence.Contexts;
using Azure.Core;
using MediatR;
using IUstaFinalProject.Domain.Entities.Identity;
using IUstaFinalProject.Application.Enums;

namespace IUstaFinalProject.Infrastructure.Services
{
    public class LoginRegisterService : ILoginRegisterService
    {
        private readonly AppDbContext _context;
        public LoginRegisterService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<string> Login(UserDto request,Role role)
        {
            var user = _context.Customers.FirstOrDefault(u => u.Username == request.UserName) ??
                throw new Exception("Username or password is wrong!");

            if (!PasswordHash.ConfirmPasswordHash(request.Password, user.PassHash, user.PassSalt))
                throw new Exception("Username or password is wrong!");

            AppUser appUser = new()
            {
                Name = request.UserName,
                Role = role.ToString(),
            };


            var token = JWTTokenService.CreateToken(appUser);
            return token;
        }

        public async Task<bool> Register(UserDto request,Role role)
        {
            if (_context.Customers.Any(u => u.Username == request.UserName))
                throw new Exception($"{request.UserName} user is already created!");

            PasswordHash.Create(request.Password, out byte[] PassHash, out byte[] PassSalt);

            Customer customer = new()
            {
                Username = request.UserName,
                PassHash = PassHash,
                PassSalt = PassSalt,
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
