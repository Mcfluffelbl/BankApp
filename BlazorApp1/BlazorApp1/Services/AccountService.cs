using Bankapp2.Domain;
using Bankapp.Interfaces;

namespace BlazorApp1.Services
{
    public class AccountService : IAccountService
    {
        // Här sparar vi konton i minnet (du kan senare byta till databas)
        private readonly List<BankAccount> _accounts = new();

        // Skapa nytt konto
        public BankAccount CreateAccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance = 0m)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Account name cannot be empty.", nameof(name));

<<<<<<< Updated upstream
        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(Guid.NewGuid(), name, accountType, currency, initialBalance);
=======
            var account = new BankAccount(name, accountType, currency, initialBalance);
>>>>>>> Stashed changes
            _accounts.Add(account);
            return account;
        }

        // Hämta alla konton (read-only)
        public IReadOnlyList<BankAccount> GetAllAccounts()
        {
<<<<<<< Updated upstream
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
=======
            return _accounts.AsReadOnly();
        }

        // Hämta konto via ID
        public BankAccount? GetAccountById(Guid accountId)
        {
            return _accounts.FirstOrDefault(a => a.Id == accountId);
        }

        // Sätt in pengar
        public void Deposit(Guid accountId, decimal amount, string? note = null)
        {
            var account = GetAccountById(accountId) ?? throw new ArgumentException("Account not found.", nameof(accountId));
            account.Deposit(amount, note);
        }

        // Ta ut pengar
        public void Withdraw(Guid accountId, decimal amount, string? note = null)
        {
            var account = GetAccountById(accountId) ?? throw new ArgumentException("Account not found.", nameof(accountId));
            account.Withdraw(amount, note);
        }

        // Överföring mellan två konton
        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount, string? note = null)
        {
            if (fromAccountId == toAccountId)
                throw new InvalidOperationException("Cannot transfer to the same account.");

            var fromAccount = GetAccountById(fromAccountId) ?? throw new ArgumentException("Source account not found.", nameof(fromAccountId));
            var toAccount = GetAccountById(toAccountId) ?? throw new ArgumentException("Destination account not found.", nameof(toAccountId));

            fromAccount.Withdraw(amount, note);
            toAccount.Deposit(amount, note);
        }

        // Ta bort konto
        public bool DeleteAccount(Guid accountId)
        {
            var account = GetAccountById(accountId);
            if (account == null)
                return false;

            return _accounts.Remove(account);
>>>>>>> Stashed changes
        }
    }
}