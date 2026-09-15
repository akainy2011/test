using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class WeatherPresenter : MonoBehaviour, IInitializable
{
    [SerializeField] private WeatherView _view;

    [Inject] private RequestQueue _requestQueue;
    [Inject] private WeatherModel _weatherModel;

    public void Initialize()
    {
        _weatherModel.OnDataLoaded += OnWeatherDataLoaded;
        LoadWeatherData();
    }

    private void OnDestroy()
    {
        _weatherModel.OnDataLoaded -= OnWeatherDataLoaded;
    }

    private void LoadWeatherData()
    {
        var request = new StubWeatherRequest();
        _requestQueue.Enqueue(request, onComplete: () => { });
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