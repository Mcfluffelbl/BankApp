namespace BlazorApp1.Services
{
    public class AccountService : IAccountService
    {
        // Här sparar vi konton i minnet (du kan senare byta till databas)
        private readonly List<BankAccount> _accounts = new();
        private readonly IStorageService _storageService;
        private bool isLoaded;

        public AccountService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        private async Task IsInitialized()
        {
            if (isLoaded)
                return;

            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            _accounts.Clear();

            if (fromStorage is { Count: > 0 })
                _accounts.AddRange(fromStorage);

            isLoaded = true;
        }
        }

<<<<<<< Updated upstream
        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(Guid.NewGuid(), name, accountType, currency, initialBalance);
=======
            var account = new BankAccount(name, accountType, currency, initialBalance);
>>>>>>> Stashed changes
            _accounts.Add(account);
            return account;
        }
        public async Task<List<IBankAccount>> GetAccounts()
        public async Task<List<BankAccount>> GetAccounts()
        {
<<<<<<< Updated upstream
            await IsInitialized();
            return _accounts.Cast<IBankAccount>().ToList();
        }

        public async Task DeleteAccount(IBankAccount account)
        {
            await IsInitialized();
            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                _accounts.Remove(accountToRemove);
                await SaveAsync();
        }

        public async Task UpdateAccounts(List<IBankAccount> updatedAccounts)
        {
            await IsInitialized();
            _accounts.Clear();
            _accounts.AddRange(updatedAccounts.Cast<BankAccount>());
            await SaveAsync();
            }
        }
    }
}