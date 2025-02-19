using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public bool Status { get; set; }
    }
}