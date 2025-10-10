using BlazorApp1.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAccountService
{
    List<IBankAccount> GetAccounts();
    Task CreateAccountAsync(string name, AccountType accountType, string currency, decimal initialBalance);
}

public class AccountService : IAccountService
{
    private readonly List<IBankAccount> _accounts = new();

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

// Example implementation of IBankAccount
public class BankAccount : IBankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }

    public void WithDraw(decimal amount)
    {
        Balance -= amount;
        LastUpdated = DateTime.Now;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        LastUpdated = DateTime.Now;
    }
}
