using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{

    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
