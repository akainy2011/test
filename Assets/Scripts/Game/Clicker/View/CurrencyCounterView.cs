using TMPro;
using UnityEngine;

public class CurrencyCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetCurrency(int value)
    {
        _text.text = $"{value}";
    }
}
