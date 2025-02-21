using FinalProject.Application.DTOs.Payment;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IStripeService
    {
        Task<string> ProcessPaymentAsync(string userId, PaymentRequestDto paymentRequestDto);
    }
}
