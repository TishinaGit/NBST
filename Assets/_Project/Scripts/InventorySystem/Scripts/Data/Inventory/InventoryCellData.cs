using System;
using Inventory;
using UnityEngine;

namespace Data.Inventory
{
    [Serializable]
    public class InventoryCellData
    {
        public int CellIndex;
        public int ID;
        public ItemTypeEnum Type;
        public Sprite AvatarItem;
        public int Count;
    }
}