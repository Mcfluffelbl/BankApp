namespace BlazorApp1.Domain
{
    public interface IBankAccount
    {
        string Name { get; }
        AccountType AccountType { get; } 
        string Currency { get; }
        decimal Balance { get; }
    }
}

