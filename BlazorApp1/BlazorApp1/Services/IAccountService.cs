using BlazorApp1.Domain;

namespace BlazorApp1.Services
{
    public interface IAccountService
    {
        IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);

        List<IBankAccount> GetAccounts();
    }
}
