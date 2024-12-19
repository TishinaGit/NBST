using Inventory;
using System; 
using UnityEngine;
 
namespace Task
{
    [Serializable]
    public class ItemTaskConfig
    {
        public ItemTypeEnum ItemTypeEnum;
        public string DescriptionItem;
        public Sprite AvatarItem;
        public int Count;
    } 
}
