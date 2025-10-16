namespace BlazorApp1.Domain
{
    public class Transaction
    {
<<<<<<< Updated upstream
=======
        public Guid AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Transaction(Guid accountId, TransactionType transactionType, decimal amount, DateTime date, string description)
        {
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            Date = date;
            Description = description;
        }
>>>>>>> Stashed changes
    }
}
