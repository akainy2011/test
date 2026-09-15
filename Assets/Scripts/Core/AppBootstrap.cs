using UnityEngine;
using Zenject;

public class AppBootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    void Start()
    {
        var context = gameObject.AddComponent<DiContext>();
        var installer = gameObject.AddComponent<AppInstaller>();

        // Загрузка GameConfig из Resources если не назначен в Inspector
        if (_gameConfig == null)
        {
            _gameConfig = Resources.Load<GameConfig>("GameConfig");
        }

        if (_gameConfig != null)
        {
            installer._gameConfig = _gameConfig;
        }

        ZenjectInstaller.Install(typeof(AppInstaller), context);
    }
}
