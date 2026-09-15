using Zenject;

public class RequestQueueInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<RequestQueue>().AsSingle();
    }
}
