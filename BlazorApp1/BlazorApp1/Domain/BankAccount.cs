using System.Text.Json.Serialization;

namespace BlazorApp1.Domain
    /// <summary>
    /// 
    /// </summary>
{
    public class BankAccount : IBankAccount
    {
        // Constants
        public Guid Id { get; private set;  } = Guid.NewGuid();
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastUpdated { get; set; }

        private readonly List<Transaction> _transactions = new();

        public IReadOnlyList<Transaction> Transactions => _transactions;

        // Constructor
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The Deposit can be 0 or less");

            // Deposit to account
            Balance += amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.Deposit,
                Date = DateTime.Now,
                FromAccount = Id,
                ToAccount = null,
                BalanceAfterTransaction = Balance
            });
        }

        /// <summary>
        /// Withdraw a specifik amount from the account balance
        /// </summary>
        /// <param name="amount">The specific amount</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException">An exception if amount is incorrect</exception>
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount withdrawn needs to be bigger than 0");

            if (Balance < amount)
                throw new InvalidOperationException("Insufficient amount");

            // Withdraw from account
            Balance -= amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction
            {
                Amount = -amount, // Negativt belopp vid uttag
                TransactionType = TransactionType.Withdrawal,
                Date = DateTime.Now,
                FromAccount = Id,
                BalanceAfterTransaction = Balance
            });
        }

        /// <summary>
        /// Transfer a specific amount from an account to an account
        /// </summary>
        /// <param name="to"></param>
        /// <param name="amount"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void Transfer(BankAccount to, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount transferred needs to be bigger than 0");

            if (Balance < amount)
                throw new InvalidOperationException("Insufficient amount");

            // From account
            Balance -= amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction
            {
                Amount = -amount, // Negativt belopp för utgående transfer
                TransactionType = TransactionType.Transfer,
                Date = DateTime.Now,
                FromAccount = Id,
                ToAccount = to.Id,
                BalanceAfterTransaction = Balance
            });

            // To account
            to.Balance += amount;
            to.LastUpdated = DateTime.Now;
            to._transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.Transfer,
                Date = DateTime.Now,
                FromAccount = Id,
                ToAccount = to.Id,
                BalanceAfterTransaction = to.Balance
            });
        }
    }
}
