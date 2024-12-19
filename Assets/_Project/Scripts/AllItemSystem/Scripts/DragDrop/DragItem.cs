using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform ParentAfterDrag;
    public Image ImageRayTarget;
    public void OnBeginDrag(PointerEventData eventData)
    {   
        ParentAfterDrag = transform.parent; 
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        ImageRayTarget.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentAfterDrag);
        ImageRayTarget.raycastTarget = true; 
    }
}
