using System.Collections.Generic;
using UnityEngine;
 
namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemSpriteDataList", menuName = "Data/ItemDataSpriteList")]
    public class ItemUIList : ScriptableObject
    {
        public string sheetId;
        public string gridId;

        public List<ItemSpriteData> ItemsSprite;
#if UNITY_EDITOR
        [ContextMenu("Sync")]
        [System.Obsolete]
        private void SyncItem()
        {
            ReadGoogleSheets.FillData<ItemSpriteData>(sheetId, gridId, listItem =>
            {
                ItemsSprite = listItem;
                ReadGoogleSheets.SetDirty(this);
            });
        }

        [ContextMenu("OpenSheet")]
        private void Open()
        {
            ReadGoogleSheets.OpenUrl(sheetId, gridId);
        }
#endif
    }

}
