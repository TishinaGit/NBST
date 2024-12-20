using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public Canvas PanelSettingsCanvas;

    public override void InstallBindings()
    {
        PanelSettings();
    }

    public void PanelSettings()
    {
        Container.Bind<Canvas>().FromInstance(PanelSettingsCanvas).AsSingle();
    }
}
