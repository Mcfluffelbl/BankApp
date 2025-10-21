namespace BlazorApp1.Domain
{
    public class Transaction
    {
        public Guid AccountId { get; } = Guid.NewGuid();
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public Guid? TransferReciver { get; set; }

        public Guid? ToAccount { get; set; }
        public Guid? FromAccount { get; set; }
        public Transaction(Guid accountId, TransactionType transactionType, decimal amount, DateTime date, string description)
        {
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            Date = date;
            Description = description;
        }
    }
}
