using System.Collections.Generic;
using System.Threading.Tasks;
namespace BlazorApp1.Interface;
public interface IAccountService
{
    Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<IBankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    Task UpdateAccounts(List<IBankAccount> updatedAccounts);
}

