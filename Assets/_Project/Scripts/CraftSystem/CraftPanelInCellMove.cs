using UnityEngine;
using UnityEngine.EventSystems;
using System;

using Inventory; 
 
namespace CraftSystem
{
    public class CraftPanelInCellMove : MonoBehaviour, IDropHandler, IPointerClickHandler 
    {
        [SerializeField] private Crafts _crafts;
        [SerializeField] private InventoryPanel _inventoryPanel;

        public static event Action SearchDataCell; 

        private GameObject _dropped;
        private Transform _lastCell;
         
        public void OnDrop(PointerEventData eventData)
        {
            // Drop
            _dropped = eventData.pointerDrag;
            var draggableItem = _dropped.GetComponent<DragItem>();
            _lastCell = draggableItem.ParentAfterDrag;
            draggableItem.ParentAfterDrag = transform;
            //\

            //search InventoryCell for _cellItemData.List<InventoryCell> 
            var cell = eventData.pointerDrag;
            var cellInventory = cell.GetComponent<InventoryCell>();
            _crafts.CellGet(cellInventory); 
            //\

            SearchDataCell?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                var removeCell = _dropped.GetComponent<InventoryCell>();
                eventData.pointerDrag = _dropped;

                if (_lastCell != null)
                {
                    _dropped.transform.SetParent(_lastCell);
                }
                 
                if (removeCell != null)
                {
                    _crafts.RemoveCellItemDataFromList(removeCell);
                }

                if (_lastCell != null)
                {
                    _lastCell.transform.SetParent(_dropped.transform);
                }

                SearchDataCell?.Invoke();
            } 
        } 
    }
}