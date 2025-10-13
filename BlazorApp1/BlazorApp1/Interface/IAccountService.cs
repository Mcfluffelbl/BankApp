using System.Collections.Generic;
using System.Threading.Tasks;
namespace BlazorApp1.Interface;
public interface IAccountService
{
    Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialbalance);
    Task<List<BankAccount>> GetAccounts();
}

/*public class AccountService : IAccountService
{
    private readonly List<IBankAccount> _accounts = new();
    private readonly IStorageService _storageService;

    private bool isLoaded;

    public AccountService(IStorageService storageService) => _storageService = storageService;

    public List<IBankAccount> GetAccounts()
    {
        return _accounts;
    }

    public async Task CreateAccountAsync(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        // Simulate async operation
        await Task.Delay(100);

        var account = new BankAccount
        {
            Id = Guid.NewGuid(),
            Name = name,
            AccountType = accountType,
            Currency = currency,
            Balance = initialBalance,
            LastUpdated = DateTime.Now
        };

        _accounts.Add(account);
    }
}
*/
// Example implementation of IBankAccount

