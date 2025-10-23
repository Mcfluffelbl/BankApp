﻿namespace BlazorApp1.Interface
{
    /// <summary>
    /// Interface containing the BankAccount methods
    /// </summary>
    public interface IBankAccount
    {
        Guid Id { get; } 
        string Name { get; }
        AccountType AccountType { get; }
        string Currency { get; }
        decimal Balance { get; }
        DateTime LastUpdated { get; }
        IReadOnlyList<Transaction> Transactions { get; }

        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void Transfer(BankAccount to, decimal amount);
    }
}

