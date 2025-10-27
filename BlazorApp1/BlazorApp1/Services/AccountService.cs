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

        public async Task<BankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance) //Liknande transaktion?
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task DeleteAccount(BankAccount account)
        {
            await IsInitialized();
            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                _accounts.Remove(accountToRemove);
                await SaveAsync();
            }
        }

        public async Task UpdateAccounts(List<BankAccount> updatedAccounts)
        {
            await IsInitialized();
            _accounts.Clear();
            _accounts.AddRange(updatedAccounts.Cast<BankAccount>());
            await SaveAsync();
        }

        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(a => a.Id == fromAccountId);
            var toAccount = _accounts.FirstOrDefault(a => a.Id == toAccountId);
            if (fromAccount == null)
                throw new ArgumentException("From account not found");
            if (toAccount == null)
                throw new ArgumentException("To account not found");
            fromAccount.Transfer(toAccount, amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
       
        }

        public void Deposit(Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var toAccount = _accounts.FirstOrDefault(a => a.Id == toAccountId);
            if (toAccount == null)
                throw new ArgumentException("Account not found");

            toAccount.Deposit(amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
           
        }

        public void Withdraw(Guid fromAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(a => a.Id == fromAccountId);
            if (fromAccount == null)
                throw new ArgumentException("Account not found");

            fromAccount.Withdraw(amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
            
        }
    }
}