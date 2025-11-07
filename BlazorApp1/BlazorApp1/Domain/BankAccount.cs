using System.Text.Json.Serialization;

namespace BlazorApp1.Domain
{
    /// <summary>
    /// Represents a bank account with basic financial operations such as deposit, withdrawal, transfer, and interest calculation.
    /// </summary>
    public class BankAccount : IBankAccount
    {
        // Constants
        public Guid Id { get; private set;  } = Guid.NewGuid();
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<Transaction> Transactions { get; private set; } = new();
        public decimal InterestRate { get; set; } = 3m;

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
        public BankAccount(Guid id, string name, AccountType accountType, string currency, decimal balance, List<Transaction> transactions)
        {
            Id = id;
            Name = name;
            AccountType = accountType;
            Currency = currency;
            Balance = balance;
            LastUpdated = DateTime.Now;
            Transactions = transactions ?? new List<Transaction>(); 
        }

        /// <summary>
        /// Deposit a specifik amount to the account balance.
        /// </summary>
        /// <param name="amount"> The amount to deposit </param>
        /// <exception cref="ArgumentException"> Thrown when the amount is zero or negative </exception>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("The amount needs to be bigger than 0 to deposit");
                throw new ArgumentException("The Deposit can´t be 0 or less");
            }

            // Deposit to account
            Balance += amount;
            LastUpdated = DateTime.Now;
            Transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.Deposit,
                Date = DateTime.Now,
                FromAccount = Id,
                ToAccount = null,
                BalanceAfterTransaction = Balance
            });
            Console.WriteLine($"Depisited to {Id} = {amount} from {Balance}. ");
        }

        /// <summary>
        /// Withdraws a specified amount from the account.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="category">Optional category for the transaction.</param>
        /// <exception cref="ArgumentException">Thrown when the amount is zero or negative.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the balance is insufficient.</exception>
        public void Withdraw(decimal amount, CategoriesType? category = null)
        {
            if (amount <= 0)
            {
                Console.WriteLine("The amount needs to be bigger than 0 to withdraw");
                throw new ArgumentException("The amount withdrawn needs to be bigger than 0");
            }
            if (Balance < amount)
            {
                Console.WriteLine("Insufficient amount to withdraw, it needs to be less than current balance");
                throw new InvalidOperationException("Insufficient amount");
            }
            
            // Withdraw from account.
            Balance -= amount;
            LastUpdated = DateTime.Now;

            // Record transaction with category.
            Transactions.Add(new Transaction
            {
                Amount = -amount,
                TransactionType = TransactionType.Withdrawal,
                Date = DateTime.Now,
                FromAccount = Id,
                BalanceAfterTransaction = Balance,
                Category = category
            });
            Console.WriteLine($"Withdrawn from {Id} = {amount} from {Balance}. ");
        }

        /// <summary>
        /// Transfer a specific amount from this account to another.
        /// </summary>
        /// <param name="to"> The recipient account </param>
        /// <param name="amount"> The amount to transfer </param>
        /// <exception cref="ArgumentException"> Thrown when the amount is zero or negative </exception>
        /// <exception cref="InvalidOperationException"> Thrown when the balance is insufficient </exception>
        public void Transfer(BankAccount to, decimal amount)
        {
            // Amount given is or less than 0 give exception.
            if (amount <= 0)
            {
                Console.WriteLine("The amount needs to be bigger than 0 to transfer");
                throw new ArgumentException("The amount transferred needs to be bigger than 0");
            }
            
            // If balance is less than the amount throw exception.
            if (Balance < amount)
            {
                Console.WriteLine("Insufficient amount to transfer, it needs to be less than current balance");
                throw new InvalidOperationException("Insufficient amount");
            }
             
            // From account
            Balance -= amount;
            LastUpdated = DateTime.Now;
            Transactions.Add(new Transaction
            {
                Amount = -amount,
                TransactionType = TransactionType.Transferout,
                Date = DateTime.Now,
                FromAccount = Id,
                ToAccount = to.Id,
                BalanceAfterTransaction = Balance
            });

            // To account
            to.Balance += amount;
            to.LastUpdated = DateTime.Now;
            to.Transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.Transferin,
                Date = DateTime.Now,
                FromAccount = Id,
                ToAccount = to.Id,
                BalanceAfterTransaction = to.Balance
            });
            Console.WriteLine($"Transfer {amount} from {Id} to {to.Id}");
        }

        /// <summary>
        /// Applies annual interest to the account balance based on the current interest rate.
        /// </summary>
        public void ApplyYearlyInterest()
        {
            var interest = Balance * (InterestRate / 100);
            Balance += interest;
            LastUpdated = DateTime.Now;

            Transactions.Add(new Transaction
            {
                Amount = interest,
                TransactionType = TransactionType.Interest,
                Date = DateTime.Now,
                FromAccount = Id,
                BalanceAfterTransaction = Balance
            });
            Console.WriteLine($"Applied interest {interest} to {Id}");
        }
    }
}
