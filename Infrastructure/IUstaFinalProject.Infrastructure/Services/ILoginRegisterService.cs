using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Domain.Entities.Dtos;

namespace IUstaFinalProject.Infrastructure.Services
{
    public interface ILoginRegisterService
    {
        Task<bool> Register(RegisterDto request,Role role,string? catId);
        Task<string> Login(UserDto request,Role role);
    }
}
