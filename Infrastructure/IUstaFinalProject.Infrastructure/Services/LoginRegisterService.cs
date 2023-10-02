using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using IUstaFinalProject.Persistence.Contexts;
using IUstaFinalProject.Domain.Entities.Identity;
using IUstaFinalProject.Application.Enums;

namespace IUstaFinalProject.Infrastructure.Services
{
    public class LoginRegisterService : ILoginRegisterService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public LoginRegisterService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<string> Login(UserDto request, Role role)
        {
            var user = new User();
            if (role == Role.Customer)
            {
                user = _context.Customers.FirstOrDefault(u => u.Username == request.UserName) ??
                throw new Exception("Username or password is wrong!");
            }
            else if(role == Role.Worker)
            {
                user = _context.Workers.FirstOrDefault(u => u.Username == request.UserName) ??
                throw new Exception("Username or password is wrong!");
            }
            else
            {
                user = _context.Admins.FirstOrDefault(u => u.Username == request.UserName) ??
               throw new Exception("Username or password is wrong!");
            }


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
                    Role = role.ToString(),
                    CreatedDate = DateTime.Now,
                    Email= request.Email,
                };
                await _context.Customers.AddAsync(customer);
            }
            else if (role == Role.Worker)
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
                    Role = role.ToString(),
                    CreatedDate = DateTime.Now,
                    Email = request.Email,
                };
                await _context.Workers.AddAsync(worker);

            }
            else
            {
                if (_context.Admins.Any(u => u.Username == request.UserName))
                    throw new Exception($"{request.UserName} Admin is already created!");

                Admin admin = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.UserName,
                    PassHash = PassHash,
                    PassSalt = PassSalt,
                    Name = request.Name,
                    Surname = request.Surname,
                    Role = role.ToString(),
                    CreatedDate= DateTime.Now,
                    Email = request.Email,
                };
                await _context.Admins.AddAsync(admin);
            }
            await _context.SaveChangesAsync();


            return true;
        }
    }
}
