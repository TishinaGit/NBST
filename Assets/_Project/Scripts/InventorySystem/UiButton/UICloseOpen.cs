using UnityEngine;
using UnityEngine.UI;

public class UICloseOpen : MonoBehaviour
{
    public Canvas InventoryPanel;
    public Image InventoryCloseAndOpen;


    private void Awake()
    {
        InventoryPanel.gameObject.SetActive(false);
    }

    public void BTM_OnUIInventory()
    {
        InventoryPanel.gameObject.SetActive(true);
        InventoryCloseAndOpen.gameObject.SetActive(false);
    }
    public void BTM_OffUIInventory()
    {
        InventoryPanel.gameObject.SetActive(false);
        InventoryCloseAndOpen.gameObject.SetActive(true);
    }
}
