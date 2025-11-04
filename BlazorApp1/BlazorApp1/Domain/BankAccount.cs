using System.Text.Json.Serialization;

namespace BlazorApp1.Domain
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
        public List<Transaction> Transactions { get; private set; } = new();

        //Ny:
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
        /// Deposit a specifik amount to the account balance
        /// </summary>
        /// <param name="amount"> The specific amount </param>
        /// <exception cref="ArgumentException"> An exception if amount is or less than 0 </exception>
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
        /// Withdraw a specifik amount from the account balance
        /// </summary>
        /// <param name="amount">The specific amount</param>
        /// <exception cref="ArgumentException"> An exception if amount is or less than 0 </exception>
        /// <exception cref="InvalidOperationException">An exception if amount is incorrect</exception>
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
            
            // Withdraw from account
            Balance -= amount;
            LastUpdated = DateTime.Now;

            // Add transaction with category
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
        /// Transfer a specific amount from an account to an account
        /// </summary>
        /// <param name="to"> The account to recive </param>
        /// <param name="amount"> The specific amount</param>
        /// <exception cref="ArgumentException"> An exception if amount is or less than 0 </exception>
        /// <exception cref="InvalidOperationException"> If amount is less than balance trhow Exception </exception>
        public void Transfer(BankAccount to, decimal amount)
        {
            // Amount given is or less than 0 give exception
            if (amount <= 0)
            {
                Console.WriteLine("The amount needs to be bigger than 0 to transfer");
                throw new ArgumentException("The amount transferred needs to be bigger than 0");
            }
            
            // If balance is less than the amount throw exception
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

        //NY:
        public void ApplyYearlyInterest()
        {
            var interest = Balance * (InterestRate / 100);
            Balance += interest;
            LastUpdated = DateTime.Now;
        }
    }
}
