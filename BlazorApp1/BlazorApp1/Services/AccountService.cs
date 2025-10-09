
using BlazorApp1.Domain;

namespace BlazorApp1.Services;

public class AccountService : IAccountService
{
    private readonly List<IBankAccount> _accounts;
    public IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        var account = new BankAccount(name, accountType, currency, initialBalance);
        _accounts.Add(account);
        return account;
    }

    public List<IBankAccount> GetAccounts()
    {
        throw new NotImplementedException();
    }
}
