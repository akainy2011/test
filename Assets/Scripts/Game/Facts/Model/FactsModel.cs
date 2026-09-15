using System;
using UnityEngine;

public class FactData
{
    public string Title { get; set; }
    public string Body { get; set; }
}

public class FactsModel : MonoBehaviour
{
    public event Action OnDataLoaded;
    private FactData[] _data;

    public FactData[] GetData() => _data;

    public void SetData(FactData[] data)
    {
        _data = data;
        OnDataLoaded?.Invoke();
    }
}
