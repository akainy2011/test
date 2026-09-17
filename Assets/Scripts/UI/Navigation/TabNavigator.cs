using System;
using System.Collections.Generic;
using UnityEngine;

public class TabNavigator : MonoBehaviour
{
    [SerializeField] private List<BasePresenter> _tabContents = new();
    [SerializeField] private List<TabButton> _tabButtons = new();

    private int _currentTab = 0;

    public void Initialize(List<string> tabLabels, Action<int> onTabSelected = null)
    {
        for (int i = 0; i < _tabButtons.Count; i++)
        {
            var label = i < tabLabels.Count ? tabLabels[i] : $"Tab {i + 1}";
            _tabButtons[i].Setup(i, label, OnTabButtonClicked);
        }

        SwitchTab(0);
    }

    private void OnTabButtonClicked(int index)
    {
        SwitchTab(index);
    }

    public void SwitchTab(int index)
    {
        if (index < 0 || index >= _tabContents.Count) return;

        _currentTab = index;

        for (int i = 0; i < _tabContents.Count; i++)
        {
            if(i == index)
                _tabContents[i].Activate();
            else
                _tabContents[i].Deactivate();
            if (i < _tabButtons.Count)
                _tabButtons[i].SetActive(i == index);
        }
     
    }

}
