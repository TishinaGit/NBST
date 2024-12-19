using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Inventory; 
 
namespace Task
{
    public class SubmitTask : MonoBehaviour
    {
        [SerializeField] private TakeTask _takeTask;
        [SerializeField] private ListTaskSo _listTaskSo;
        [SerializeField] private InventoryPanel _inventoryPanel;
        [SerializeField] private List<InventoryCell> _SlotItemData;
        [SerializeField] private List<UITaskData> _uiTasks; 
        [SerializeField] private List<InventoryCell> _suitableItem;

        private int _maxAmountTask = 3;
        public int InStockInt = 0;

        private void OnEnable()
        {
            CheckInventory();
            ComparisonItems();
        }

        private void OnDisable()
        {
            RemoveListSuitableItem();
            InStockInt = 0;
        }

        public void CheckInventory()
        {
            foreach (var task in _uiTasks)
            {
                SlotCheckInStock(task);
            }
        }

        private void SlotCheckInStock(UITaskData task)
        {
            int _countPotionInt = int.Parse(task._countPotion.text); 
            var dataCell = _SlotItemData.FirstOrDefault(slot => task._itemType == slot.CurrentData.Type &&
                                                        _countPotionInt == slot.CurrentData.Count ||
                                                        task._itemType == slot.CurrentData.Type &&
                                                        _countPotionInt <= slot.CurrentData.Count);

            if (dataCell != null)
            {
                _suitableItem.Add(dataCell);
            }
        } 

        private void ComparisonItems()
        { 
            var task = _listTaskSo.ListTasks[_takeTask._saveIndexCurrentTask];

            for (int i = 0; i < _suitableItem.Count; i++)
            {
                if (task.listTasks[i].ItemTypeEnum == _suitableItem[i].CurrentData.Type)
                {
                    InStockInt++;
                }
            }  
        }

        public void BTM_PassTask()
        {
            if (InStockInt == _maxAmountTask)
            {
                RemoveItems();
                RemoveListSuitableItem();
                _takeTask.PlusIndexList(1);
                InStockInt = 0;
            }
        }

        private void RemoveItems()
        {
            var task = _listTaskSo.ListTasks[_takeTask._saveIndexCurrentTask];
            
            for (int i = 0; i < _suitableItem.Count; i++)
            {  
                for (int j = 0; j < task.listTasks[i].Count; j++)
                {
                    _inventoryPanel.RemoveItem(_suitableItem[i].CurrentData.Type, task.listTasks[i].Count);
                }
                
            }
        }

        private void RemoveListSuitableItem()
        {
            if (_suitableItem != null)
            {
                _suitableItem.Clear();
            }
                
        }
    }
}

