using System;
using Inventory;
using UnityEngine;

namespace Data.Inventory
{
    [Serializable]
    public struct InventoryCellData
    {
        public int CellIndex;
        public ItemTypeEnum Type;
        public Sprite AvatarItem;
        public int Count;
    }
}