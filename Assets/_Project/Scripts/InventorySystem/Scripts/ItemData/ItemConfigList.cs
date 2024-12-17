using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemConfigList", menuName = "Config/ItemConfigList")]
    public class ItemConfigList : ScriptableObject
    {
        public string sheetId;
        public string gridId;

        public List<ItemConfig> items;
#if UNITY_EDITOR
        [ContextMenu("Sync")]
        [System.Obsolete]
        private void SyncItem()
        {
            ReadGoogleSheets.FillData<ItemConfig>(sheetId, gridId, listItem =>
            {
                items = listItem;
                
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
