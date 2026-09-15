using Zenject;

public class ClickerModuleInstaller : Installer
{
    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private CurrencyCounterView _currencyCounterView;
    [SerializeField] private EnergyCounterView _energyCounterView;
    [SerializeField] private ClickerPresenter _clickerPresenter;

    public override void InstallBindings()
    {
        // Models -- singletons
        Container.Bind<ClickerModel>().AsSingle();
        Container.Bind<EnergyModel>().AsSingle();

        // Views -- from scene references
        Container.Bind<ClickerView>().FromInstance(_clickerView).AsSingle();
        Container.Bind<CurrencyCounterView>().FromInstance(_currencyCounterView).AsSingle();
        Container.Bind<EnergyCounterView>().FromInstance(_energyCounterView).AsSingle();

        // Presenter
        Container.Bind<ClickerPresenter>().AsSingle();
    }
}
