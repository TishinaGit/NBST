using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; 

using Inventory;
 
namespace CraftSystem
{
    public class GetItemAfterCreation : MonoBehaviour
    {
        [SerializeField] private Crafts _crafts;

        public Sprite _itemAvatar;
        public ItemTypeEnum ItemType;
        public TMP_Text CountText;
        public Image AvatarItem;

        public bool isBTMClick = false;

        private void Update()  
        {
            CheckBool();
        }

        public void SetPropertiesCreateItem(ItemTypeEnum itemTypeEnum, string text, Sprite sprite)
        {
            ItemType = itemTypeEnum;
            CountText.text = text;
            AvatarItem.sprite = sprite;
        }

        public void BTM_TakeItem()
        {
            isBTMClick = true;
            _crafts.CraftPotion();
            _crafts.ItemTakeCheck();
        }

        private void CheckBool()
        {
            if (isBTMClick == true)
            {
                StartCoroutine(UpdateBool());
            }
        }

        private IEnumerator UpdateBool()
        { 
            yield return new WaitForSeconds(0.2f);
            isBTMClick = false;
        }
    }
}
