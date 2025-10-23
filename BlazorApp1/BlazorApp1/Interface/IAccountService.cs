namespace BlazorApp1.Interface;
public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(BankAccount account);
    Task UpdateAccounts(List<BankAccount> updatedAccounts);
    
    void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    void Deposit(Guid toAccountId, decimal amount);
    void Withdraw(Guid fromAccountId, decimal amount);
}

