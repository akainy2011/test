using System;

public class ClickerModel
{
    public int Currency { get; private set; } = 0;
    public event Action<int> OnCurrencyChanged;

    public void AddCurrency(int amount)
    {
        Currency += amount;
        OnCurrencyChanged?.Invoke(Currency);
    }

    public void SetCurrency(int amount)
    {
        Currency = amount;
        OnCurrencyChanged?.Invoke(Currency);
    }
}
