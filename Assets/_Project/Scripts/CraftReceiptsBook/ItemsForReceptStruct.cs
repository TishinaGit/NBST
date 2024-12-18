using System;
using UnityEngine;

using Inventory;

namespace ReceiptsSystem
{
    [Serializable]
    public class ItemsForReceptStruct
    {
        public ItemTypeEnum ItemType;
        public Sprite SpriteItem;
        public int Count;
        public bool isInStock = false;
    }
}
