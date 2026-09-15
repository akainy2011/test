using System;
using UnityEngine;

public class WeatherView : MonoBehaviour
{
    public event Action OnRefreshRequested;

    [SerializeField] private Transform _contentParent;

    public void Clear()
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddWeatherCard(string city, int temperature, string icon)
    {
        // TODO: Instantiate WeatherCard prefab
        Debug.Log($"[WeatherView] Add: {city} {temperature}°C [{icon}]");
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
