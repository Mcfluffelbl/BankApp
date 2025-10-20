using System.Text.Json.Serialization;
using BlazorApp1.Interface;

namespace BlazorApp1.Domain
{
    public class BankAccount : IBankAccount
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public AccountType AccountType { get; private set; }
        public string Currency { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Currency { get; private set; } = string.Empty;
        public decimal Balance { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public List<Transaction> Transactions { get; private set; } = new();

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
