namespace BlazorApp1.Services
{
    public class AccountService : IAccountService
    {
        private const string StorageKey = "BlazorApp1.accounts";
        private readonly List<BankAccount> _accounts = new();
        private readonly IStorageService _storageService;

        private bool isLoaded;

        public AccountService(IStorageService storageService) => _storageService = storageService;

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
                isLoaded = true;
            }     
        }

        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(Guid.NewGuid(), name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task<List<BankAccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<BankAccount>().ToList();
        }
    }
}