using AutoMapper;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Account;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FinalProject.Application.Abstractions.Token;
using FinalProject.Application.DTOs.Tokens;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        //private readonly IEmailService _emailService;
        //private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<AppUser> userManager,
            IMapper mapper,
            ITokenHandler tokenhandler,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenHandler = tokenhandler;

        }

        public Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
        {
            
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>loginDto.EmailOrUsername==u.UserName || loginDto.EmailOrUsername==u.Email);
            if (user is null)
                throw new Exception("Username ,email or Password incorrect");

            bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                user.AccessFailedCount++;
                throw new Exception("Username ,email or Password incorrect");
            }


      

            
            //var roles = await _userManager.GetRolesAsync(user);

            
            return _tokenHandler.CreateAccessToken(user,15);

        }

        public async Task RegisterAsync(RegisterDto userDto)
        {
          if(await _userManager.Users.AnyAsync(u=>u.UserName==userDto.UserName|| u.Email == userDto.Email))
            {
                throw new Exception("this user already exists");
            }
            if (!userDto.IsAgree)
            {
                throw new Exception("You must agree to the terms and conditions to register");
            }

            var result = await _userManager.CreateAsync(_mapper.Map<AppUser>(userDto),userDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder builder = new();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }

                throw new Exception(builder.ToString());
            }

           
        }

        public Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
