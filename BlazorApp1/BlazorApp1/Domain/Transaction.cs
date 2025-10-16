namespace BlazorApp1.Domain
{
    public class Transaction
    {
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
    }
}
