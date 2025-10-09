using BlazorApp1.Interface;

namespace BlazorApp1.Domain
{
    public class BankAccount : IBankAccount
    {
        public Guid Id {  get; private set; } = Guid.NewGuid();
        public AccountType AccountType { get; private set; }
        public string Name {  get; private set; }
        public string Currency { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public BankAccount(string name, AccountType accountType, string currency, decimal initialbalance)
        {
            Name = name;
            AccountType = accountType;
            Currency = currency;
            Balance = initialbalance;
            LastUpdated = DateTime.Now;
        }

        public void WithDraw(decimal amount)
        {
            throw new NotImplementedException();
        }
        public void Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }  
    }
}
