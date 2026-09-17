using System;

public class WeatherModel
{
    public event Action OnDataLoaded;
    private WeatherData[] _data;

    public WeatherData[] GetData() => _data;

    public void SetData(WeatherData[] data)
    {
        _data = data;
        OnDataLoaded?.Invoke();
    }
}