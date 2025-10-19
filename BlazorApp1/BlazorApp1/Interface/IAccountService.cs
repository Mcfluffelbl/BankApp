using System.Collections.Generic;
using System.Threading.Tasks;
namespace BlazorApp1.Interface;
public interface IAccountService
{
    Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialbalance = 0);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    
    Task<IReadOnlyList<BankAccount>> GetAccountsAsync();
    Task DepositAsync(Guid accountId, decimal amount, string? note = null);
    Task WithdrawAsync(Guid accountId, decimal amount, string? note = null);
    Task<IReadOnlyList<Transaction>> GetTransactionsAsync(Guid accountId);
}

