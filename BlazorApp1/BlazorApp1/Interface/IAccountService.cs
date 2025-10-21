namespace BlazorApp1.Interface;
public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    Task UpdateAccounts(List<IBankAccount> updatedAccounts);
    
    void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
}

