using System.Collections.Generic;
using System.Threading.Tasks;
namespace BlazorApp1.Interface;
public interface IAccountService
{
    Task<BankAccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialbalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
}

