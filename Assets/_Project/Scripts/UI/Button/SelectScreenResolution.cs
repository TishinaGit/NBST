using TMPro;
using UnityEngine;

public class SelectScreenResolution : MonoBehaviour
{
    [SerializeField] private GameObject _closePanel;
    [SerializeField] private TMP_Text _settingText;
    public void BTM_Display19x10()
    {
        Screen.SetResolution(1920, 1080, true);
        _settingText.text = "1920x1080";
        ClosePanel();
    }

    public void BTM_Display25x14()
    {
        Screen.SetResolution(2560, 1440, true);
        _settingText.text = "2560x1440";
        ClosePanel();
    }

    public void BTM_Display10x7()
    {
        Screen.SetResolution(1024, 768, true);
        _settingText.text = "1024x768";
        ClosePanel();
    }

    private void ClosePanel()
    {
        _closePanel.SetActive(false);
    }
}
