namespace BlazorApp1.Services
{
    /// <summary>
    /// Service responsible for managing bank accounts, including creation, retrieval,
    /// updates, transfers, deposits, withdrawals, and interest application.
    /// </summary>
    public class AccountService : IAccountService
    {
        // Fields
        private const string StorageKey = "BlazorApp1.accounts";
        private readonly List<BankAccount> _accounts = new();
        private readonly IStorageService _storageService;
        private bool isLoaded;

        /// <summary>
        /// Initializes a new instance of the Accountservice class.
        /// </summary>
        /// <param name="storageService"> An implementation of IStorageService for handling persistent storage </param>
        public AccountService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Ensures that the account list is loaded from storage before performing operations.
        /// </summary>
        private async Task IsInitialized()
        {
            if (isLoaded)
            {
                return;
            }

            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            _accounts.Clear();

            if (fromStorage is { Count: > 0 })
            {
                _accounts.AddRange(fromStorage);
            }
            isLoaded = true;
        }

        /// <summary>
        /// Saves the current account list to persistent storage.
        /// </summary>
        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        /// <summary>
        /// Creates a new bank account and saves it to storage.
        /// </summary>
        /// <param name="name"> The account name </param>
        /// <param name="accountType"> The type of account </param>
        /// <param name="currency"> The currency for the account </param>
        /// <param name="initialBalance"> The initial account balance </param>
        /// <returns> The newly created Bankaccount </returns>
        public async Task<BankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance) //Liknande transaktion?
        {
            await IsInitialized();
            var account = new BankAccount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        /// <summary>
        /// Retrieves all bank accounts from memory or storage.
        /// </summary>
        /// <returns> A list of BankAccount objects </returns>
        public async Task<List<BankAccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<BankAccount>().ToList();
        }

        /// <summary>
        /// Deletes a specified account and updates the stored data.
        /// </summary>
        /// <param name="account"> The account to delete </param>
        public async Task DeleteAccount(BankAccount account)
        {
            await IsInitialized();
            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                Console.WriteLine("Deleted account from storage");
                _accounts.Remove(accountToRemove);
                await SaveAsync();
            }
        }

        /// <summary>
        /// Replaces all existing accounts with the provided updated list and saves to storage.
        /// </summary>
        /// <param name="updatedAccounts"> The list of updated accounts </param>
        public async Task UpdateAccounts(List<BankAccount> updatedAccounts)
        {
            await IsInitialized();
            _accounts.Clear();
            _accounts.AddRange(updatedAccounts.Cast<BankAccount>());
            await SaveAsync();
        }

        /// <summary>
        /// Transfers a specified amount from one account to another.
        /// </summary>
        /// <param name="fromAccountId"> The ID of the source account </param>
        /// <param name="toAccountId"> The ID of the destination account </param>
        /// <param name="amount"> The amount to transfer </param>
        /// <exception cref="ArgumentException"> If Account not found throw exception </exception>
        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(a => a.Id == fromAccountId);
            var toAccount = _accounts.FirstOrDefault(a => a.Id == toAccountId);
            if (fromAccount == null)
            {
                Console.WriteLine("Account not found when transfer from account");
                throw new ArgumentException("From account not found");
            }
            if (toAccount == null)
            {
                Console.WriteLine("Account not found when transfer to account");
                throw new ArgumentException("To account not found");
            }
            fromAccount.Transfer(toAccount, amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
        }

        /// <summary>
        /// Deposits a specified amount into a given account.
        /// </summary>
        /// <param name="toAccountId"> The ID of the account to deposit into </param>
        /// <param name="amount"> The amount to deposit </param>
        /// <exception cref="ArgumentException"> If Account not found throw exception </exception>
        public void Deposit(Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var toAccount = _accounts.FirstOrDefault(a => a.Id == toAccountId);
            if (toAccount == null)
            {
                Console.WriteLine("Account not found when depositing");
                throw new ArgumentException("Account not found");
            }
            toAccount.Deposit(amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
        }

        /// <summary>
        /// Withdraws a specified amount from a given account.
        /// </summary>
        /// <param name="fromAccountId"> The ID of the account to withdraw from </param>
        /// <param name="amount"> The amount to withdraw </param>
        /// <param name="category">Optional transaction category </param>
        /// <exception cref="ArgumentException"> If Account not found throw exception </exception>
        public void Withdraw(Guid fromAccountId, decimal amount, CategoriesType? category)
        {
            var fromStorage = _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(a => a.Id == fromAccountId);

            if (fromAccount == null)
            {
                Console.WriteLine("Account was not found when withdrawing");
                throw new ArgumentException("Account not found");
            }

            fromAccount.Withdraw(amount, category);

            _storageService.SetItemAsync(StorageKey, _accounts);
        }

        /// <summary>
        /// Applies yearly interest to a savings account.
        /// </summary>
        /// <param name="accountId"> The ID of the account to apply interest to </param>
        /// <returns></returns>
        public async Task ApplyInterest(Guid accountId)
        {
            await IsInitialized();

            var account = _accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null && account.AccountType == AccountType.Savings)
            {
                account.ApplyYearlyInterest();
                await SaveAsync();
                Console.WriteLine($"Applied interest to account {account.Name}");
            }
        }
    }
}