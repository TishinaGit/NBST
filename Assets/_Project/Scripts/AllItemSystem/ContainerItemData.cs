using System.Collections.Generic;
using UnityEngine; 

public class ContainerItemData : MonoBehaviour
{ 
    [SerializeField] private GameObject[] _slotsItem; 

    public List<GameObject> _itemsData;
    private void Update()
    {
        BTM_ActiveInventoryPanel(); 
    }
    public void BTM_ActiveInventoryPanel()
    {
        if (Input.GetKey(KeyCode.E))
        {  
            GiveItesmInSlotInventory();
        } 
    }

    public void GiveItesmInSlotInventory()
    { 
        for (int i = 0; i < _itemsData.Count; i++)
        {
            _itemsData[i].transform.SetParent(_slotsItem[i].transform);
            _itemsData[i].transform.localScale = Vector3.one;  
        }
    }
}
