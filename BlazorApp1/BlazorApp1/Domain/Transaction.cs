using System.Text.Json.Serialization;

namespace BlazorApp1.Domain
{
    public class Transaction
    {
<<<<<<< HEAD
        public Guid Id { get; } = Guid.NewGuid();
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid? TransferReciver { get; set; }
        public Transaction(TransactionType transactionType, decimal amount, DateTime date)
        {
            TransactionType = transactionType;
            Amount = amount;
            Date = date;
        }
=======
        // Constants
        public Guid Id { get; } = Guid.NewGuid();
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal BalanceAfterTransaction { get; set; }
        public Guid? ToAccount { get; set; }
        public Guid? FromAccount { get; set; }
        public CategoriesType? Category { get; set; }
>>>>>>> Workplace1.2
    }
}
