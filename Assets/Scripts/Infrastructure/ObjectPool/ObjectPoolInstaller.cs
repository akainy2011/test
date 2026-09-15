using Zenject;

public class ObjectPoolInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolManager>().AsSingle();
    }
}
