using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DropItemInCell : MonoBehaviour, IDropHandler
    { 
        private int _cellIndex;

        public void OnDrop(PointerEventData eventData) 
        {  
            var dropped = eventData.pointerDrag;
            var draggableItem = dropped.GetComponent<DragItem>();
            var FirstitemDroppedCellIndex = dropped.GetComponent<InventoryCell>();

            if (transform.childCount > 0)
            {
                var current = transform.GetChild(0).gameObject;
                var currentDraggable = current.GetComponent<DragItem>();
                var SeconditemDroppedCellIndex = currentDraggable.GetComponent<InventoryCell>();

                _cellIndex = FirstitemDroppedCellIndex.CurrentData.CellIndex;
                FirstitemDroppedCellIndex.CurrentData.CellIndex = SeconditemDroppedCellIndex.CurrentData.CellIndex;
                SeconditemDroppedCellIndex.CurrentData.CellIndex = _cellIndex;

                currentDraggable.transform.SetParent(draggableItem.ParentAfterDrag);
                draggableItem.ParentAfterDrag = transform;
            }
           
        } 
    } 
} 