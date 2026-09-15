using System;
using TMPro;
using UnityEngine;

public class EnergyCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    public event Action<int> OnValueChanged;

    private int _currentValue;

    public void SetEnergy(int current, int max)
    {
        _currentValue = current;
        _text.text = $"{current}/{max}";
        OnValueChanged?.Invoke(current);
    }
}
