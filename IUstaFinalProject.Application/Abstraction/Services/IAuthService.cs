using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Abstraction.Services
{
    public interface IAuthService
    {
        //Task PasswordResetAsnyc(string email);
        //Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
        Task<Dtos.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<Dtos.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
