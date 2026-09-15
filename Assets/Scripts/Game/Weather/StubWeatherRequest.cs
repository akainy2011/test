using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

// Stub weather request for demonstration
public class StubWeatherRequest : IQueuedRequest<List<WeatherData>>
{
    public string Id => "StubWeatherRequest";
    UniTask IQueuedRequest.Execute()
    {
        return Execute();
    }

    public async UniTask<List<WeatherData>> Execute()
    {
        // Simulate network delay
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        var data = new List<WeatherData>
        {
            new WeatherData { City = "New York", Temperature = 22, Icon = "sun" },
            new WeatherData { City = "London", Temperature = 15, Icon = "rain" },
            new WeatherData { City = "Tokyo", Temperature = 28, Icon = "cloud" },
        };

        // Set data to model
        var weatherModel = Object.FindObjectOfType<WeatherModel>();
        weatherModel?.SetData(data.ToArray());

        return data;
    }
}
