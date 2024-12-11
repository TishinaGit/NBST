using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class ItemConfig
    {
        public string Name;
        public Sprite AvatarItem;
        public string DescriptionItem;
        public ItemTypeEnum Type;
        public int Count;
        public ItemPotionEnum ItemPotionEnum;
    }
} 