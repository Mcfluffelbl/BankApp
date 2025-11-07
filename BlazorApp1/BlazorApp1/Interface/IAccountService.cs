namespace BlazorApp1.Interface;
/// <summary>
/// Interface contaning the Accountservice methods.
/// </summary>
public interface IAccountService
{
<<<<<<< HEAD
    Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialbalance = 0);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    
    Task<IReadOnlyList<BankAccount>> GetAccountsAsync();
    Task DepositAsync(Guid accountId, decimal amount, string? note = null);
    Task WithdrawAsync(Guid accountId, decimal amount, string? note = null);
    Task<IReadOnlyList<Transaction>> GetTransactionsAsync(Guid accountId);
=======
    Task<BankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(BankAccount account);
    Task UpdateAccounts(List<BankAccount> updatedAccounts);
    void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    void Deposit(Guid toAccountId, decimal amount);
    void Withdraw(Guid fromAccountId, decimal amount, CategoriesType? category);
    Task ApplyInterest(Guid accountId);
>>>>>>> Workplace1.2
}

