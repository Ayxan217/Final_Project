namespace FinalProject.Infrastructure.Implementations.Services
{
    internal class Payment
    {
        public string UserId { get; set; }
        public object Amount { get; set; }
        public object Currency { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
    }
}