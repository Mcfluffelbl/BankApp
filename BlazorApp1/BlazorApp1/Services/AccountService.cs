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

        public async Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, List<Transaction> transactions, decimal initialBalance = 0)
        {
            await IsInitialized();
            var account = new BankAccount(Guid.NewGuid(), name, accountType, currency, initialBalance, transactions = new List<Transaction>());
            
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task<List<BankAccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<BankAccount>().ToList();
        }

        public async Task DeleteAccount(IBankAccount account)
        {
            await IsInitialized();
            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (account != null)
            {
                _accounts.Remove(accountToRemove);
                await SaveAsync();
            }
        }

        public async Task<IReadOnlyList<BankAccount>> GetAccountsAsync()
        {
            await IsInitialized();
            return _accounts.AsReadOnly();
        }

        public async Task DepositAsync(Guid accountId, decimal amount, string? note = null)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(a => a.Id == accountId)
            ??throw new InvalidOperationException("Account not found.");
            account.Deposit(amount, note);
            await SaveAsync();

        }

        public async Task WithdrawAsync(Guid accountId, decimal amount, string? note = null)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(a => a.Id == accountId)
            ?? throw new InvalidOperationException("Account not found.");
            account.WithDraw(amount, note);
            await SaveAsync();
        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionsAsync(Guid accountId)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(a => a.Id == accountId)
            ?? throw new InvalidOperationException("Account not found.");

            return account.transactions.AsReadOnly();
        }
    }
}