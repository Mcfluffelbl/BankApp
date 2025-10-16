namespace BlazorApp1.Services
{
    public class AccountService : IAccountService
    {
        private const string StorageKey = "BlazorApp1.accounts";
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

        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(Guid.NewGuid(), name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task<List<IBankAccount>> GetAccounts()
        {
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