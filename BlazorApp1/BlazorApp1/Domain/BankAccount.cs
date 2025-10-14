using System.Text.Json.Serialization;

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

        public BankAccount(string name, AccountType accountType, string currency, decimal balance)
        {
            Name = name;
            AccountType = accountType;
            Currency = currency;
            Balance = balance;
            LastUpdated = DateTime.Now;
        }

        [JsonConstructor]
        public BankAccount(Guid id, string name, AccountType accountType, string currency, decimal balance) 
        { 
            Id = id;
            Name = name;
            Currency = currency;
            Balance = balance;
            AccountType = accountType;
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
