using TMPro;
using UnityEngine;

public class EnergyCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetEnergy(int current, int max)
    {
        _text.text = $"{current}/{max}";
    }
}
