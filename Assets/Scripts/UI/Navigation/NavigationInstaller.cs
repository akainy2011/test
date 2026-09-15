using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NavigationInstaller : Installer
{
    [SerializeField] private TabNavigator _tabNavigator;

    public override void InstallBindings()
    {
        Container.Bind<TabNavigator>().FromInstance(_tabNavigator).AsSingle();
    }
}
