using IUstaFinalProject.Application.Abstraction.Services;
using IUstaFinalProject.Application.Abstraction.Token;
using IUstaFinalProject.Application.Dtos;
using IUstaFinalProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IUstaFinalProject.Persistence.Services
{
    public class AuthService:IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly IUserService _userService;
        public AuthService(
            IConfiguration configuration,
            UserManager<Domain.Entities.Identity.AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }
       

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new Exception("User not found");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) 
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new Exception("Auth error");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
                throw new Exception("Error occurred");
        }

        //public async Task PasswordResetAsnyc(string email)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        //        //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
        //        //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
        //        resetToken = resetToken.UrlEncode();

        //        await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
        //    }
        //}

        //public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        //{
        //    AppUser user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
        //        //resetToken = Encoding.UTF8.GetString(tokenBytes);
        //        resetToken = resetToken.UrlDecode();

        //        return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        //    }
        //    return false;
        //}
    }
}

