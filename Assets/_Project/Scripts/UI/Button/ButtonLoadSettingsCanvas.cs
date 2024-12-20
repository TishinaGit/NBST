using UnityEngine;
using Zenject;

public class ButtonLoadSettingsCanvas : MonoBehaviour
{ 
    [SerializeField] private GameObject _mainMenuCanvas;
    public GameObject _settingsCanvas;

    [Inject]
    public void Construct(GameObject PanelSettingsCanvas)
    {
        this._settingsCanvas = PanelSettingsCanvas;
    }

    public void BTM_OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
    }

    public void BTM_CloseSettings()
    {
        _settingsCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}
