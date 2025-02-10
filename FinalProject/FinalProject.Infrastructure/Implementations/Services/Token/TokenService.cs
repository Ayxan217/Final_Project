using FinalProject.Application.Abstractions.Token;
using FinalProject.Application.DTOs.Account;
using FinalProject.Application.DTOs.Tokens;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FinalProject.Persistence.Implementations.Token
{
    public class TokenService : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration configuration,
            UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<TokenResponseDto> CreateAccessToken(AppUser user, int minutes)
        {
            var secretKey = _configuration["JWT:SecretKey"]
        ?? throw new InvalidOperationException("JWT:SecretKey configuration is missing");
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));


            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
           var role =roles.First();
            var claims = new List<Claim>
        {


            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role,role),


        };


            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            }



            var expiration = DateTime.Now.AddMinutes(minutes);


            JwtSecurityToken securityToken = new(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: claims
            );


            JwtSecurityTokenHandler tokenHandler = new();


            return new TokenResponseDto(tokenHandler.WriteToken(securityToken),
                 CreateRefreshToken(),
                 securityToken.ValidTo);



        }

        public string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }


    }
}

