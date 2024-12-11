using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Inventory;
using CraftSystem;

namespace ReceiptsSystem
{
    public class BookPanelItemRecepiptsCraft : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private CraftSO _craftSO;
        [SerializeField] private GameObject _craftReceiptsPanel;
        [SerializeField] private Image[] _itemReceiptsData;
        [SerializeField] private GameObject _transformPanel;
        public ItemTypeEnum ItemType;

        public void GFD()
        {
            foreach (var item in _craftSO.ReceiptsPotions)
            {
                if (ItemType == item.ItemPotionType)
                {
                    var a = item.itemsForReceiptstStructs[0];
                    var b = item.itemsForReceiptstStructs[1];
                    _itemReceiptsData[0].sprite = a.SpriteItem;
                    _itemReceiptsData[1].sprite = b.SpriteItem;
                    _craftReceiptsPanel.SetActive(true);
                    _craftReceiptsPanel.transform.position = _transformPanel.transform.position;
                }
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            GFD();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _craftReceiptsPanel.SetActive(false);
        }


    }
}
