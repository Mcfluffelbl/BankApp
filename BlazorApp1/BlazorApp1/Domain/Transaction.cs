namespace BlazorApp1.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Transaction(Guid accountId, TransactionType transactionType, decimal amount, DateTime date, string description, string status)
        {
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            Date = date;
            Description = description;
            Status = status;
        }
    }
}
