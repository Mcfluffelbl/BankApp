﻿namespace BlazorApp1.Domain
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

        void WithDraw(decimal amount);
        void Deposit(decimal amount);
    }
}

