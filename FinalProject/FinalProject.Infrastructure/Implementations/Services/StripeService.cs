using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payment;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Implementations.Services
{
    internal class StripeService : IStripeService
    {
      
        public async Task<string> CreatePaymentIntent(
            CreatePaymentDto paymentDto
            
            )
        {
            var amount = paymentDto.amount * 100;
            var paymentIntentService = new PaymentIntentService();
            var paymentMethodService = new PaymentMethodService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = (long)amount, 
                Currency = paymentDto.currency,
                PaymentMethodTypes = new List<string> { "card" }
             
            });
            var paymentMethod = await paymentMethodService.CreateAsync(new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = paymentDto.CardNumber,
                    ExpMonth = paymentDto.ExpMont,
                    ExpYear = paymentDto.ExpYear,
                    Cvc = paymentDto.CVC
                }
            });


            return paymentIntent.Id;
        }

        public async Task<bool> VerifyPayment(string paymentIntentId)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.GetAsync(paymentIntentId);
            return paymentIntent.Status == "succeeded";
        }
    }
}
