using FinalProject.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using SendGrid;
using PostmarkDotNet;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        private const string APIKEY = "3355d1ea-dabd-407c-87c2-3637f466cdc1";

        public  async Task SendEmailAsync(string to, string subject, string body)
        {
            var client = new PostmarkClient(APIKEY);

            var message = new PostmarkMessage
            {
                From = "ayxanrm-bp217@code.edu.az",
                To = "memmedliayxan@gmail.com",
                Subject = "Reset",
                HtmlBody = "<strong>Hello</strong> dear Postmark user.",
                TextBody = "Reset your email"
            };

            var response = await client.SendMessageAsync(message);

            if (response.Status != PostmarkStatus.Success)
            {
                throw new Exception($"Email gönderimi başarısız: {response.Message}");
            }
        }
    }
}
