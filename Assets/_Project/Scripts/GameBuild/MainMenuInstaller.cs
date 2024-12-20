using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public GameObject PanelSettingsCanvas;

    public override void InstallBindings()
    {
        PanelSettings();
    }

    public void PanelSettings()
    {
        Container.Bind<GameObject>().FromInstance(PanelSettingsCanvas).AsSingle();
    }
}
