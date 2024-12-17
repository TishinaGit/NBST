using Data;  
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private List<InventoryCell> _inventoryCells = new();
        [SerializeField] protected Test _tets;

        private void Awake()
        {
            InitInventoryCell(); 
        }

        private void OnEnable()
        {
            OnLoad();
            
        }

        private void OnDisable()
        {
            Save();
        } 

        public void InitInventoryCell()
        {
            for (int i = 0; i < _inventoryCells.Count; i++)
            {
                _inventoryCells[i].InitIndex(i);
            }
        }
         

        
        #region AddAndRemoveItemInventory
        public void AddItem(ItemTypeEnum itemTypeEnum, int count , int ID)
        {
            InventoryCell firstEmptyCell = null;

            foreach (var inventoryCell in _inventoryCells)
            {
                if (firstEmptyCell == null && inventoryCell.CurrentData.Type == ItemTypeEnum.None)
                {
                    firstEmptyCell = inventoryCell;
                    _tets.GiveSpriteItem();
                    Save();
                    inventoryCell.ReDraw();
                    DataCentralService.Instance.InventoryStates.UpdateCellData(inventoryCell.CurrentData);
                }

                if (firstEmptyCell != null)
                {
                    firstEmptyCell.AddNewItem(itemTypeEnum, count,  ID);
                    _tets.GiveSpriteItem();
                    Save();
                    inventoryCell.ReDraw();
                    DataCentralService.Instance.InventoryStates.UpdateCellData(inventoryCell.CurrentData);
                    return;
                }

                if (inventoryCell.CurrentData.Type == itemTypeEnum)
                {
                    inventoryCell.AddCountItem(count);
                     _tets.GiveSpriteItem();
                    Save();
                    inventoryCell.ReDraw();
                    DataCentralService.Instance.InventoryStates.UpdateCellData(inventoryCell.CurrentData);
                    return;
                }
                
            } 

            //TODO: Если некуда впихнуть что то сделать
        }

        public void RemoveItem(ItemTypeEnum itemTypeEnum, int count)
        {
            foreach (var inventoryCell in _inventoryCells)
            {
                if (inventoryCell.CurrentData.Type == itemTypeEnum)
                {

                    if (inventoryCell.CurrentData.Count > count)
                    {
                        inventoryCell.RemoveCountItem(count);
                        Debug.Log("removeCount");
                        Save();
                        inventoryCell.ReDraw();
                        DataCentralService.Instance.InventoryStates.UpdateCellData(inventoryCell.CurrentData); 
                        break;
                    }

                    if (inventoryCell.CurrentData.Count == count)
                    {
                        inventoryCell.CurrentData.Type = ItemTypeEnum.None;
                        inventoryCell.CurrentData.Count = 0;
                        inventoryCell.CurrentData.AvatarItem = null;
                        Debug.Log("==Count");
                        Save();
                        inventoryCell.ReDraw();
                        DataCentralService.Instance.InventoryStates.UpdateCellData(inventoryCell.CurrentData); 
                        break;
                    }
                     
                    if (inventoryCell.CurrentData.Count <= count)
                    {
                        Debug.Log("Не успешно");
                        Save();
                        inventoryCell.ReDraw();
                        DataCentralService.Instance.InventoryStates.UpdateCellData(inventoryCell.CurrentData);
                        return;
                    }
                }  
            } 
        }
        #endregion

        #region SaveAndLoadCells
        public void Save()
        {
            if (_inventoryCells != null)
            {
                for (int i = 0; i < _inventoryCells.Count; i++)
                {
                    SaveCells(i);
                }
            }
            DataCentralService.Instance.SaveStates(); 
        }

        private void SaveCells(int id)
        {
            var data = _inventoryCells[id].CurrentData;
            data.Count = _inventoryCells[id].CurrentData.Count;
            data.Type = _inventoryCells[id].CurrentData.Type;
            data.ID = _inventoryCells[id].CurrentData.ID;
            DataCentralService.Instance.InventoryStates.UpdateCellData(data);
        }

        public void OnLoad()
        {
            foreach (InventoryCell inventoryCell in _inventoryCells)
            {
                inventoryCell.Load();
            }
        }
        #endregion

    }
}
