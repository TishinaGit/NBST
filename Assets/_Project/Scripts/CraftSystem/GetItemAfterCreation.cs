using UnityEngine;
using UnityEngine.UI;
using TMPro; 

using Inventory; 

namespace CraftSystem
{
    public class GetItemAfterCreation : MonoBehaviour
    {
        [SerializeField] private Crafts _crafts;

        public Sprite _itemAvatar;
        public ItemTypeEnum ItemType;
        public TMP_Text CountText;
        public Image AvatarItem ;

        public bool isBTMClick = false;
        public float time;

        private void Update() //Ref!!!
        {
            UpdateTimeBool();
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
            SetPropertiesCreateItem(ItemTypeEnum.None, " ", _itemAvatar); 
        }

        public void UpdateTimeBool()
        {
            time = 0.3f;
            if (isBTMClick == true)
            {
                time -= 12 * Time.deltaTime;
                if (time <= 0)
                {
                    isBTMClick = false;
                    time = 0.3f;
                }
            }
        }
    }
}
