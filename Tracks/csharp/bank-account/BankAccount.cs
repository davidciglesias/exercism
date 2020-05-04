using System;

public class BankAccount
{
    private static T Lock<T>(Func<T> func) { lock (accountLock) { return func(); } }

    private bool isOpen = false;
    public void Open() => Lock(() => isOpen = true);
    public void Close() => Lock(() => isOpen = false);

    private readonly static object accountLock = new object();

    private decimal balance;
    public decimal Balance
    {
        get => Lock(() => !isOpen ? throw new InvalidOperationException() : balance);
        private set => Lock(() => balance = value);
    }

    public void UpdateBalance(decimal change) => Lock(() => balance += !isOpen ? throw new InvalidOperationException() : change);
}
