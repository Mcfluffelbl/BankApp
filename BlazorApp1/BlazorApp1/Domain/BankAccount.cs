using System.Text.Json.Serialization;

namespace BlazorApp1.Domain
{
    public class BankAccount : IBankAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<Transaction> Transactions { get; set; } = new();

        public BankAccount() { }

        public BankAccount(Guid id, string name, AccountType accountType, string currency, decimal initialBalance)
        {
            Id = id;
            Name = name;
            AccountType = accountType;
            Currency = currency;
            Balance = initialBalance;
            LastUpdated = DateTime.Now;
        }
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The Deposit can be 0 or less");

            Balance += amount;
            LastUpdated = DateTime.Now;

            Transactions.Add(new Transaction(Id, TransactionType.Deposit, amount, DateTime.Now, "Deposit"));
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount withdrawn needs to be bigger than 0");

            if (Balance < amount)
                throw new InvalidOperationException("Incinifcent amount");

            Balance -= amount;
            LastUpdated = DateTime.Now;

            Transactions.Add(new Transaction(Id, TransactionType.Withdrawal, amount, DateTime.Now, "Withdraw"));
        }
    }
}
