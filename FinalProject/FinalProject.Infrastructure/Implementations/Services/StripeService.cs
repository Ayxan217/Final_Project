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
      
            private readonly IUnitOfWork _unitOfWork;  // UnitOfWork ile veri kaydetme işlemi yapılır
            private readonly IConfiguration _configuration;

        public StripeService(IUnitOfWork unitOfWork, IConfiguration configuration)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<string> ProcessPaymentAsync(string userId, PaymentRequestDto paymentRequest)
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                // Stripe ile token'ı doğrula ve ödeme işlemini başlat
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = (long)(paymentRequest.Amount * 100), // Stripe cent cinsinden alır
                    Currency = paymentRequest.Currency,
                    Source = paymentRequest.Token,
                    Description = "Hizmet Bedeli"
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);

                // Ödeme durumu kontrolü
                if (charge.Status == "succeeded")
                {
                    // Ödeme başarılıysa veritabanına kaydet
                    var payment = new Payment
                    {
                        UserId = userId,
                        Amount = paymentRequest.Amount,
                        Currency = paymentRequest.Currency,
                        TransactionId = charge.Id,
                        Status = "Success"
                    };

                    await _unitOfWork.PaymentRepository.AddAsync(payment);
                    await _unitOfWork.CommitAsync();

                    return "Ödeme başarılı!";
                }
                else
                {
                    // Ödeme başarısızsa hata döndür
                    return "Ödeme başarısız!";
                }
            }
        }
    }

