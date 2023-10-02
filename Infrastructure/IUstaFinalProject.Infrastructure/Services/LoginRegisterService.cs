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
using System.Net.Http.Headers;

namespace IUstaFinalProject.Infrastructure.Services
{
    public class LoginRegisterService : ILoginRegisterService
    {
        private readonly AppDbContext _context;
        public LoginRegisterService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<string> Login(UserDto request, Role role)
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


        public async Task<bool> Register(RegisterDto request, Role role, string? categoryId)
        {
            PasswordHash.Create(request.Password, out byte[] PassHash, out byte[] PassSalt);

            if (role == Role.Customer)
            {
                if (_context.Customers.Any(u => u.Username == request.UserName))
                    throw new Exception($"{request.UserName} user is already created!");
                Customer customer = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.UserName,
                    PassHash = PassHash,
                    PassSalt = PassSalt,
                    Name = request.Name,
                    Surname = request.Surname,
                    Role = role.ToString()
                };

                await _context.Customers.AddAsync(customer);
            }
            else
            {

                if (_context.Workers.Any(u => u.Username == request.UserName))
                    throw new Exception($"{request.UserName} user is already created!");

                Worker worker = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.UserName,
                    PassHash = PassHash,
                    PassSalt = PassSalt,
                    Name = request.Name,
                    Surname = request.Surname,
                    CategoryId = Guid.Parse(categoryId),
                    Role = role.ToString()
                };
                await _context.Workers.AddAsync(worker);

            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                throw;
            }
            
            return true;
        }
    }
}
