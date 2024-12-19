using Data;
using Data.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryCell : MonoBehaviour
    { 
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Image _iconItem;
        [SerializeField] private Sprite _spriteTransparent;
        [SerializeField] protected AddSpriteForItem _addSpriteForItem;
        public InventoryCellData CurrentData;
        public int Index;
         
        public void InitIndex(int index)
        {
            Index = index;
        }

        public void Load()
        {
            CurrentData = DataCentralService.Instance.InventoryStates.GetCellData(Index);
            ReDraw();
        }

        #region AddAndRemoveLogic
        public void AddNewItem(ItemTypeEnum itemTypeEnum, int count, int ID)
        {
            CurrentData.Type = itemTypeEnum;
            CurrentData.Count = count; 
            CurrentData.ID = ID; 

            DataCentralService.Instance.InventoryStates.UpdateCellData(CurrentData);
            ReDraw();
        }
        
        public void AddCountItem(int count)
        {
            CurrentData.Count += count;
            DataCentralService.Instance.InventoryStates.UpdateCellData(CurrentData);
            ReDraw();
        }

        public void RemoveCountItem(int count)
        {
            CurrentData.Count -= count;
            DataCentralService.Instance.InventoryStates.UpdateCellData(CurrentData);
            ReDraw();
        }
        #endregion
        public void ReDraw()
        {
            if (CurrentData.Type == ItemTypeEnum.None)
            { 
                _count.text = " ";
                _iconItem.sprite = _spriteTransparent;
            }
            else
            { 
                _count.text = CurrentData.Count.ToString();
                
                _addSpriteForItem.GiveSpriteItem();
                _iconItem.sprite = CurrentData.AvatarItem;
            }
            
        }
    }
}