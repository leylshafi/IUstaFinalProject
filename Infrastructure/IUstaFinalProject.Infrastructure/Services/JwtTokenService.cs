﻿using IUstaFinalProject.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IUstaFinalProject.Infrastructure.Services
{
    public class JWTTokenService
    {
        public static string CreateToken(AppUser user)
        {
            List<Claim> Claims = new()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my most secure security key in the world"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: Claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddSeconds(10)
                );

            var JWT = new JwtSecurityTokenHandler().WriteToken(token);
            return JWT;
        }
    }
}
