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

namespace FinalProject.Persistence.Implementations.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(_configuration["MailChimp:FromEmail"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            // Mandrill SMTP ayarları
            using var smtp = new SmtpClient
            {
                Host = "smtp.mandrillapp.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _configuration["MailChimp:Username"], // Email adresiniz
                    _configuration["MailChimp:ApiKey"]    // Mandrill API Key
                )
            };

            await smtp.SendMailAsync(message);
        }
    }
}
