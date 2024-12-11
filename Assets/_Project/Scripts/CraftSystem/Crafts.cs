using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Data;

using Inventory; 


namespace CraftSystem
{
    public class Crafts : MonoBehaviour
    {
        [SerializeField] private CraftSO _craftSO;
        [SerializeField] private GetItemAfterCreation _getItemAfterCreation;
        [SerializeField] private InventoryPanel _inventoryPanel;
        [SerializeField] private List<InventoryCell> _cellItemData; 
         
        private void OnEnable()
        {
            CraftPanelInCellMove.SearchDataCell += CraftPotion; 
        }

        private void OnDisable()
        {
            CraftPanelInCellMove.SearchDataCell -= CraftPotion; 
        }
 
        public void CraftPotion()
        {
            foreach (var receipts in _craftSO.ReceiptsPotions)
            {
                receipts.isCraft = false;
               

                foreach (var itemReceipt in receipts.itemsForReceiptstStructs)
                {
                    itemReceipt.isInStock = false;

                    var dataCell = _cellItemData.FirstOrDefault(cell =>
                                   cell.CurrentData.Type == itemReceipt.ItemType &&
                                   cell.CurrentData.Count >= itemReceipt.Count);

                    CheckingMaterials(dataCell, itemReceipt, receipts); 
                }
            }
        }  
         
        private void CheckingMaterials(InventoryCell dataCell, ItemsForReceptStruct itemReceipt, Receipts receipts)  
        { 
            receipts.isCraft = false;
            itemReceipt.isInStock = false;

            if (dataCell != null)
            {
                itemReceipt.isInStock = true;
            }

            for (var i = 0; i < receipts.itemsForReceiptstStructs.Count; i++)  //Ref!!!
            {
                if (receipts.itemsForReceiptstStructs[0].isInStock == true && 
                    receipts.itemsForReceiptstStructs[1].isInStock == true)
                {
                    receipts.isCraft = true;
                    _getItemAfterCreation.SetPropertiesCreateItem(receipts.ItemPotionType, receipts.Count.ToString(), receipts.AvatarItem);

                    CheckBTMActive(dataCell, receipts);
                }
                else
                {
                    break;
                }
            }
        } 

        private void CheckBTMActive(InventoryCell dataCell, Receipts receipts)
        { 
            if (_getItemAfterCreation.isBTMClick == true)
            {
                CreateItem(receipts);  
                RemoveMaterial(receipts); 
                _inventoryPanel.Save();
                dataCell.ReDraw();
                DataCentralService.Instance.InventoryStates.UpdateCellData(dataCell.CurrentData);
                _getItemAfterCreation.isBTMClick = false;
            }
        } 

        public void CellGet(InventoryCell cell)
        {
            _cellItemData.Add(cell);
        } 

        private void CreateItem(Receipts receipt)
        {
            _inventoryPanel.AddItem(receipt.ItemPotionType, receipt.Count, receipt.AvatarItem);  
        } 

        public void RemoveMaterial(Receipts receipts)
        {
            foreach (var RemoveData in _cellItemData)
            {
                _inventoryPanel.RemoveItem(RemoveData.CurrentData.Type, receipts.Count);
            }
        }

        public void RemoveCellItemDataFromList(InventoryCell cellData)
        {
            _cellItemData.Remove(cellData);
        }

    }
}