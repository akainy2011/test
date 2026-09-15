using System;

public class WeatherData
{
    public string City { get; set; }
    public int Temperature { get; set; }
    public string Icon { get; set; }
}

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
