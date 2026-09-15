using Cysharp.Threading.Tasks;
using UnityEngine;

public class WeatherPresenter : MonoBehaviour
{
    [SerializeField] private WeatherView _view;

    [Inject] private RequestQueue _requestQueue;
    [Inject] private WeatherModel _weatherModel;

    private void OnEnable()
    {
        _weatherModel.OnDataLoaded += OnWeatherDataLoaded;
        LoadWeatherData();
    }

    private void OnDisable()
    {
        _weatherModel.OnDataLoaded -= OnWeatherDataLoaded;
    }

    private void LoadWeatherData()
    {
        var request = new StubWeatherRequest();
        _requestQueue.Enqueue(request, onComplete: () =>
        {
            // Data will be set by the request itself
        });
    }

    private void OnWeatherDataLoaded()
    {
        _view.Clear();
        foreach (var data in _weatherModel.GetData())
        {
            _view.AddWeatherCard(data.City, data.Temperature, data.Icon);
        }
        _view.Show();
    }
}
