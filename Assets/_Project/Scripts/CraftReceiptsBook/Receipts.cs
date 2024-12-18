using System;
using System.Collections.Generic;
using UnityEngine;

using Inventory;

namespace ReceiptsSystem
{
    [Serializable]
    public class Receipts
    {
        public Sprite AvatarItem;
        public ItemTypeEnum ItemPotionType;
        public bool isCraft = false;
        public int Count;

        public List<ItemsForReceptStruct> itemsForReceiptstStructs;
    }

}
