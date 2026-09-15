using UnityEngine;
using Zenject;

public class WeatherModuleInstaller : Installer
{
    [SerializeField] private WeatherView _weatherView;
    [SerializeField] private WeatherPresenter _weatherPresenter;

    public override void InstallBindings()
    {
        Container.Bind<WeatherModel>().AsSingle();
        Container.Bind<WeatherView>().FromInstance(_weatherView).AsSingle();
        Container.Bind<WeatherPresenter>().AsSingle();
    }
}
