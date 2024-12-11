using UnityEngine;

public class UiInventoryTypeOpenClose : MonoBehaviour
{
    public GameObject FoodInventoryPanel;
    //public GameObject WeaponInventoryPanel;
    //public GameObject ResInventoryPanel;

    private void Awake()
    {
        FoodInventoryPanel.gameObject.SetActive(true);
        //WeaponInventoryPanel.gameObject.SetActive(false);
        //ResInventoryPanel.gameObject.SetActive(false);
    }

    public void BTM_FoodInventoryPanel()
    {
        FoodInventoryPanel.gameObject.SetActive(true);

        if (FoodInventoryPanel.gameObject == true)
        {
           // WeaponInventoryPanel.gameObject.SetActive(false);
            //ResInventoryPanel.gameObject.SetActive(false);
        }

    }
    public void BTM_WeaponInventoryPanel()
    {
       // WeaponInventoryPanel.gameObject.SetActive(true);

        //if (WeaponInventoryPanel.gameObject == true)
        //{
        //    FoodInventoryPanel.gameObject.SetActive(false);
        //    ResInventoryPanel.gameObject.SetActive(false);
        //}
    }
    public void BTM_ResInventoryPanel()
    {
        //ResInventoryPanel.gameObject.SetActive(true);

        //if (ResInventoryPanel.gameObject == true)
        //{
        //    FoodInventoryPanel.gameObject.SetActive(false);
        //    WeaponInventoryPanel.gameObject.SetActive(false);
        //}
    }

}
