using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Tokens
{
    public record TokenResponseDto(string Token,DateTime Expire,string RefreshToken,DateTime RefreshTokenExpireTime);
    
}
