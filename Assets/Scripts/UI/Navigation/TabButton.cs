using System;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private GameObject _activeIndicator;

    private int _index;
    private Action<int> _onTabSelected;

    public void Setup(int index, string label, Action<int> onTabSelected)
    {
        _index = index;
        _label.text = label;
        _onTabSelected = onTabSelected;

        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => _onTabSelected?.Invoke(_index));
    }

    public void SetActive(bool active)
    {
        if (_activeIndicator != null)
            _activeIndicator.SetActive(active);
    }
}
