using System;

public class TabButton : TextButton
{
    private int _index;
    private Action<int> _onTabSelected;

    public void Setup(int index, string label, Action<int> onTabSelected)
    {
        RemoveAllListeners();
        _index = index;
        SetText(label);
        _onTabSelected = onTabSelected;
        AddClickListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _onTabSelected?.Invoke(_index);
    }

    public void SetActive(bool active)
    {
       if(active)
           Lock();
       else
           Unlock();
    }
}
