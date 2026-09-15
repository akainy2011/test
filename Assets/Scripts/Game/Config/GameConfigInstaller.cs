using Zenject;

public class GameConfigInstaller : Installer
{
    [SerializeField] private GameConfig _gameConfig;

    public override void InstallBindings()
    {
        if (_gameConfig == null)
        {
            _gameConfig = ScriptableObject.CreateInstance<GameConfig>();
        }
        Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
    }
}
