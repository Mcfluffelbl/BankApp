using System.Text.Json.Serialization;

namespace BlazorApp1.Domain
{
    public class Transaction
    {
        //
        public Guid Id { get; } = Guid.NewGuid();
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal BalanceAfterTransaction { get; set; }
        public Guid? ToAccount { get; set; }
        public Guid? FromAccount { get; set; }
    }
}
