using UnityEngine;

public class ButtonOpenPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panelDisplayResolition;

    public void BTM_OpenPanel()
    {
        _panelDisplayResolition.SetActive(true);
    }
}
