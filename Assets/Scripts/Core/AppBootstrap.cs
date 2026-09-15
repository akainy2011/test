using UnityEngine;
using Zenject;

public class AppBootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    void Start()
    {
        var container = new DiContainer();

        // Bind GameConfig first
        if (_gameConfig == null)
        {
            _gameConfig = Resources.Load<GameConfig>("GameConfig");
        }
        if (_gameConfig != null)
        {
            container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
        }

        // Manual POCO bindings (no Installer dependency)
        container.Bind<RequestQueue>().AsSingle();
        container.Bind<ClickerModel>().AsSingle();
        container.Bind<EnergyModel>().AsSingle();
        container.Bind<WeatherModel>().AsSingle();
        container.Bind<FactsModel>().AsSingle();
    }
}
