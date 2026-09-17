using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherApiRequest : IQueuedRequest<List<WeatherData>>
{
    private const string ApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
    private readonly WeatherModel _weatherModel;

    public string Id => "WeatherApiRequest";
    UniTask IQueuedRequest.Execute(CancellationToken cancellationToken)
    {
        return Execute(cancellationToken);
    }

    public WeatherApiRequest(WeatherModel weatherModel)
    {
        _weatherModel = weatherModel;
    }

    public async UniTask<List<WeatherData>> Execute(CancellationToken cancellationToken = default)
    {
        using var request = UnityWebRequest.Get(ApiUrl);
        request.timeout = 10;
        var operation = request.SendWebRequest();
     
        while (!operation.isDone && !cancellationToken.IsCancellationRequested)
        {
            await UniTask.Yield(cancellationToken: cancellationToken);
        }

        if (cancellationToken.IsCancellationRequested)
        {
            request.Abort();
            throw new OperationCanceledException();
        }

        if (request.result != UnityWebRequest.Result.Success)
            throw new Exception($"Weather API error: {request.error}");

        var response = JsonUtility.FromJson<WeatherForecastResponse>(request.downloadHandler.text);
        var data = ParseForecast(response);
        
        _weatherModel.SetData(data.ToArray());

        return data;
    }

    private List<WeatherData> ParseForecast(WeatherForecastResponse response)
    {
        var result = new List<WeatherData>();
        if (response?.properties?.periods == null) return result;

        foreach (var period in response.properties.periods)
        {
            result.Add(new WeatherData
            {
                StartTime = period.startTime,
                EndTime = period.endTime,
                TemperatureF = period.temperature,
                Icon = period.icon
            });
        }

        return result;
    }
}