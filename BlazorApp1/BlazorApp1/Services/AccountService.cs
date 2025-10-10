using BlazorApp1.Domain;

namespace BlazorApp1.Domain
{
    public class AccountService : IAccountService
    {
        private readonly List<IBankAccount> _accounts = new();

        public IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            var account = new BankAccount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            return account;
        }

        public Task CreateAccountAsync(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            throw new NotImplementedException();
        }

        public List<IBankAccount> GetAccounts()
        {
            // ✅ Returnera kontona istället för att kasta ett fel
            return _accounts;
        }
    }
}