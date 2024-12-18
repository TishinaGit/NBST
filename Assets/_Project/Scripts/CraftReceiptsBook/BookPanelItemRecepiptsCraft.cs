using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Inventory;
using CraftSystem;

namespace ReceiptsSystem
{
    public class BookPanelItemRecepiptsCraft : MonoBehaviour ,IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private CraftSO _craftSO; 
         
        [SerializeField] private GameObject _craftReceiptsPanel;
        [SerializeField] private Transform _transformPanel;
        [SerializeField] private Image[] _recipeMaterialSprites;

        public ItemTypeEnum ItemType;

        public void RecipeMaterialSpritesUI()
        {
            foreach (var item in _craftSO.ReceiptsPotions)
            {
                if (ItemType == item.ItemPotionType)
                {
                    var a = item.itemsForReceiptstStructs[0];
                    var b = item.itemsForReceiptstStructs[1];
                    _recipeMaterialSprites[0].sprite = a.SpriteItem;
                    _recipeMaterialSprites[1].sprite = b.SpriteItem;
                    _craftReceiptsPanel.SetActive(true);
                    _craftReceiptsPanel.transform.position = _transformPanel.transform.position;
                }
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            RecipeMaterialSpritesUI();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _craftReceiptsPanel.SetActive(false); 
        } 
    }
}
