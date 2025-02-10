using FinalProject.Application.DTOs.Account;
using FinalProject.Application.DTOs.Tokens;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Task<TokenResponseDto> CreateAccessToken(AppUser user,int minutes);
        string CreateRefreshToken();

    }
}
