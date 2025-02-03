using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Account
{
    public record ResetPasswordDto(
     string Email,
     string Token,
     string NewPassword
      
        );
}
