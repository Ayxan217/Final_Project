using FinalProject.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IStripeService
    {
        Task<string> ProcessPaymentAsync(string userId, PaymentRequestDto paymentRequestDto);
    }
}
