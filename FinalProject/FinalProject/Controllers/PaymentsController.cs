using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IStripeService _paymentService;

        public PaymentsController(IStripeService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromForm] CreatePaymentDto paymentDto)
        {
            try
            {
                var clientSecret = await _paymentService.CreatePaymentIntent(paymentDto);
                return Ok(new { clientSecret });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromForm] string paymentIntentId)
        {
            bool isSuccess = await _paymentService.VerifyPayment(paymentIntentId);
            return Ok(new { success = isSuccess });
        }
    }
}

