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
using FinalProject.Domain.Enums;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<AppUser> userManager,
            IMapper mapper,
            ITokenHandler tokenhandler,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenHandler = tokenhandler;
            _configuration = configuration;
            _emailService = emailService;

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

            AppUser user = _mapper.Map<AppUser>(userDto);

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

            await _userManager.AddToRoleAsync(user,Roles.Patient.ToString());
            
           
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            AppUser? user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user is null)
                throw new Exception("User not found");

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            string resetLink = $"{_configuration["App:ClientUrl"]}/reset-password?email={forgotPasswordDto.Email}&token={resetToken}";

            
            await _emailService.SendEmailAsync(
            
                to : forgotPasswordDto.Email,
                subject : "Password Reset",
                body : $"Please click the link below to reset your password:\n\n{resetLink}"
            );
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            AppUser? user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user is null)
                throw new Exception("User not found");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                StringBuilder errorBuilder = new();
                foreach (var error in result.Errors)
                {
                    errorBuilder.AppendLine(error.Description);
                }
                throw new Exception(errorBuilder.ToString());
            }
        }
    }
}
