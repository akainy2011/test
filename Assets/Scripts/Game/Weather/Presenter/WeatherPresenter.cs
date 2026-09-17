using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class WeatherPresenter : BasePresenter
{
    [SerializeField] private WeatherView _view;

    [Inject] private RequestQueue _requestQueue;
    [Inject] private WeatherModel _weatherModel;
    [Inject] private WeatherApiRequest _weatherApiRequest;

    private bool _isTabActive;
    private WeatherData _lastData;
   
    
    public override void Activate()
    {
        _view.Show();
        _weatherModel.OnDataLoaded += OnWeatherDataLoaded;
        _isTabActive = true;
        LoadWeatherData();
        StartWeatherLoop();
    }
    
    public override void Deactivate()
    {
        _view.Hide();
        _isTabActive = false;
        _weatherModel.OnDataLoaded -= OnWeatherDataLoaded;
        _requestQueue.CancelAllOfType<WeatherApiRequest>();
    }

    private void OnDestroy()
    {
        Deactivate();
    }

    private void StartWeatherLoop()
    {
        WeatherLoop();
    }

    private async UniTaskVoid WeatherLoop()
    {
        while (_isTabActive)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            if (!_isTabActive) break;
            LoadWeatherData();
        }
    }

    private void LoadWeatherData()
    {
        _requestQueue.Enqueue(_weatherApiRequest,
            onComplete: () => { },
            onError: (ex) => Debug.LogError($"Weather request failed: {ex.Message}"));
    }

    private void OnWeatherDataLoaded()
    {
        foreach (var data in _weatherModel.GetData())
        {
            var startTime = DateTime.Parse(data.StartTime);
            var endTime = DateTime.Parse(data.EndTime);
            if (DateTime.Now >= startTime && DateTime.Now <= endTime)
            {
                
                if(_lastData != null && _lastData.Icon == data.Icon && _lastData.TemperatureF == data.TemperatureF)
                    return;
                LoadIconAsync(data);
                _view.AddWeatherCard(data.TemperatureF);
                _lastData = data;
                return;
            }
        }
       
    }
    
    private async UniTaskVoid LoadIconAsync(WeatherData data)
    {
        using var request = UnityWebRequestTexture.GetTexture(data.Icon);
        await request.SendWebRequest();
    
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load icon: {data.Icon} - {request.error}");
            return;
        }
    
        var texture = DownloadHandlerTexture.GetContent(request);
        var sprite = Sprite.Create(
            texture, 
            new Rect(0, 0, texture.width, texture.height), 
            new Vector2(0.5f, 0.5f)
        );

        _view.SetIcon(sprite);
    }
}