using Inventory;
using System;
using UnityEngine;
namespace Inventory
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
