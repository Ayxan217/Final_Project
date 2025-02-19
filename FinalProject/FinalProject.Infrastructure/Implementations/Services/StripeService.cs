using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payment;
using FinalProject.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProject.Infrastructure.Implementations.Services.StripeService;

namespace FinalProject.Infrastructure.Implementations.Services
{
    internal class StripeService : IStripeService
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentRepository _paymentRepository;

        public StripeService(IPaymentRepository paymentRepository,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
        }
        public async Task<string> ProcessPaymentAsync(string userId, PaymentRequestDto paymentRequestDto)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var amount = paymentRequestDto.Amount * 100;
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = (long)amount,
                Currency = paymentRequestDto.Currency,
                Source = paymentRequestDto.Token,
                Description = "Ödeme işlemi"
            };

            var chargeService = new ChargeService();
            Charge charge = await chargeService.CreateAsync(chargeOptions);

            if (charge.Status == "succeeded")
            {
                var payment = new Payment
                {
                    UserId = userId,
                    Amount = paymentRequestDto.Amount,
                    Currency = paymentRequestDto.Currency,
                    TransactionId = charge.Id,
                    Status = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                };

                await _paymentRepository.AddAsync(payment);
                await _paymentRepository.SaveChangesAsync(); 

                return "Ödeme başarılı!";
            }
            else
            {
                return "Ödeme başarısız!";
            }
        }
    }

}

