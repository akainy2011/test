using Zenject;

public class AppInstaller : Installer
{
    public override void InstallBindings()
    {
        // Infrastructure
        Install<RequestQueueInstaller>();
        Install<ObjectPoolInstaller>();

        // Game config
        Install<GameConfigInstaller>();

        // Feature modules
        Install<ClickerModuleInstaller>();
        Install<WeatherModuleInstaller>();
        Install<FactsModuleInstaller>();

        // UI
        Install<NavigationInstaller>();
    }
}
