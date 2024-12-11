using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadSettingsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _mainmenuCanvas;

    public void BTM_OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        _mainmenuCanvas.SetActive(false);
    }

    public void BTM_CloseSettings()
    {
        _settingsCanvas.SetActive(false);
        _mainmenuCanvas.SetActive(true);
    }
}
