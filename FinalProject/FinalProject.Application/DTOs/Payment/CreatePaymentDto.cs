namespace FinalProject.Application.DTOs.Payment
{
    public record CreatePaymentDto(decimal amount, string currency,
        string CardNumber, int ExpMont, int ExpYear, string CVC);

}
