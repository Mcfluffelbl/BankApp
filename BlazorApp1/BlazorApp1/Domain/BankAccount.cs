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
        private readonly List<Transaction> _transactions = new();
        public BankAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            Name = name;
            AccountType = accountType;
            Currency = currency;
            Balance = initialBalance;
            LastUpdated = DateTime.Now;
        }
        [JsonConstructor]
        public BankAccount(Guid id, string name, AccountType accountType, string currency, decimal balance)
        {
            Id = id;
            Name = name;
            AccountType = accountType;
            Currency = currency;
            Balance = balance;
            LastUpdated = DateTime.Now;
        }
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The Deposit can be 0 or less");

            Balance += amount;
            LastUpdated = DateTime.Now;

            _transactions.Add(new Transaction { Amount = amount });
            
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount withdrawn needs to be bigger than 0");

            if (Balance < amount)
                throw new InvalidOperationException("Incinifcent amount");

            Balance -= amount;
            LastUpdated = DateTime.Now;

            _transactions.Add(new Transaction { Amount = amount });
        }

        public void Transfer(BankAccount to, decimal amount)
        {
            Balance -= amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction
            { 
                Amount = amount,
                TransactionType = TransactionType.Transferout,
                FromAccount = Id,
                ToAccount = to.Id
            });
            
            to.Balance += amount;
            to.LastUpdated = DateTime.Now;
            to._transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.Transferin,
                FromAccount = Id
            });
        }
    }
}
