using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromForm] PaymentRequestDto requestDto)
        {
            if (requestDto == null || string.IsNullOrEmpty(requestDto.Token))
                return BadRequest("Geçersiz ödeme isteği");

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("please login");
            var result = await _paymentService.ProcessPaymentAsync(userId, requestDto);

            if (result == "Ödeme başarılı!")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }
    }
}

