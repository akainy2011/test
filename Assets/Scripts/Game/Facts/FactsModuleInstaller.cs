using Zenject;

public class FactsModuleInstaller : Installer
{
    [SerializeField] private FactsView _factsView;
    [SerializeField] private FactsPresenter _factsPresenter;

    public override void InstallBindings()
    {
        Container.Bind<FactsModel>().AsSingle();
        Container.Bind<FactsView>().FromInstance(_factsView).AsSingle();
        Container.Bind<FactsPresenter>().AsSingle();
    }
}
