using Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RefreshItemBook : MonoBehaviour
{
    [SerializeField] private List<InventoryCell> _inventoryCells;
    [SerializeField] private TMP_Text _currentCount;
    public ItemTypeEnum itemTypeEnum;

   
    private void Update()
    {
        RefreshItem();
    }

    public void RefreshItem()
    {
        foreach (InventoryCell cell in _inventoryCells)
        {
            if (cell.CurrentData.Type == itemTypeEnum)
            {
                _currentCount.text = cell.CurrentData.Count.ToString();
            }
        }
    }

}
