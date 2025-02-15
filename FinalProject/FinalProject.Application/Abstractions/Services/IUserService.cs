using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);
    }
}
