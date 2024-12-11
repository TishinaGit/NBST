using UnityEngine;
using UnityEngine.UI;

public class UseItemUi : MonoBehaviour
{
    [SerializeField] private Image _buttonlight;
    [SerializeField] private GameObject _itemPanelUse;
    //[SerializeField] private FoodInventoryObject _foodInventoryObject;

    public void BTM_PodsvetkaAndPanel()
    {
        _buttonlight.gameObject.SetActive(true);
        _itemPanelUse.SetActive(true);
    }

    public void BTM_UseItem()
    {
        Debug.Log("“ут пока что ничего нет");
    }

    public void BTM_CancelUsePanel()
    {
        _buttonlight.gameObject.SetActive(false);
        _itemPanelUse.SetActive(false);
    }
}
