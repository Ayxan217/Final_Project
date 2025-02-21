namespace FinalProject.Application.DTOs.Payment
{
    public class PaymentRequestDto
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
