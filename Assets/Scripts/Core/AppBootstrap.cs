using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AppBootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    void Start()
    {
        var container = new DiContainer();

        // Bind GameConfig
        if (_gameConfig == null)
            _gameConfig = Resources.Load<GameConfig>("GameConfig");
        if (_gameConfig != null)
            container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();

        // Manual bindings
        container.Bind<RequestQueue>().AsSingle();
        container.Bind<ClickerModel>().AsSingle();
        container.Bind<EnergyModel>().AsSingle();
        container.Bind<WeatherModel>().AsSingle();
        container.Bind<DogsModel>().AsSingle();
        container.Bind<WeatherApiRequest>().AsSingle();
        container.Bind<GetBreedsRequest>().AsSingle();
        
        var poolManager = new ObjectPoolManager();
        container.Bind<ObjectPoolManager>().FromInstance(poolManager).AsSingle();

        // Inject all [Inject] fields on DIRoot and its children
        var diRoot = GameObject.Find("DIRoot");
        if (diRoot != null)
        {
            container.InjectGameObject(diRoot);

            // Initialize TabNavigator
            var tabNavigator = diRoot.GetComponentInChildren<TabNavigator>();
            if (tabNavigator != null)
                tabNavigator.Initialize(new List<string> { "Кликер", "Погода", "Факты" });

            
            var clickerPresenter = diRoot.GetComponentInChildren<ClickerPresenter>(true);
            if (clickerPresenter != null) clickerPresenter.Initialize();
        }
    }
}

// Simple IInitializable interface (not Zenject's)
public class BasePresenter: MonoBehaviour
{
    public virtual void Initialize()
    {
        
    }

    public virtual void Activate()
    {
       
    }

    public virtual void Deactivate()
    {
        
    }
}