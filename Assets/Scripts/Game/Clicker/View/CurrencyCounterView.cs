using System;
using TMPro;
using UnityEngine;

public class CurrencyCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    public event Action<int> OnValueChanged;

    private int _currentValue;

    public void SetCurrency(int value)
    {
        _currentValue = value;
        _text.text = $"{value}";
        OnValueChanged?.Invoke(value);
    }
}
