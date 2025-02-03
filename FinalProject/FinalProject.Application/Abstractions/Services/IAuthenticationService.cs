
using FinalProject.Application.DTOs.Account;
using FinalProject.Application.DTOs.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
        Task RegisterAsync(RegisterDto registerDto);
        //Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        //Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);


    }
}
