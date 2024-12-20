 using Inventory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AddSpriteForItem : MonoBehaviour
{
    [SerializeField] private ItemConfigList _itemConfigList;
    [SerializeField] private ItemUIList _itemUIList;
    public List<InventoryCell>  _inventoryCells;
     
    [Inject]
    public void Construct(List<InventoryCell> _inventoryCells)
    {
       this._inventoryCells = _inventoryCells;
    }

    private void Awake()
    {
        Init();
    }
  
    public void Init()
    {
        foreach (var item in _itemConfigList.items)
        {
            foreach (var itemSprite in _itemUIList.ItemsSprite)
            {
                if (item.ID == itemSprite.ID)
                {
                    item.ID = itemSprite.ID; 
                }
            }
        }
    }

    public void GiveSpriteItem()
    {  
        foreach (var item in _inventoryCells)
        {
            foreach (var sprite in _itemUIList.ItemsSprite)
            {
                if (item.CurrentData.ID == sprite.ID)
                {
                    item.CurrentData.AvatarItem = sprite.AvatarItem;
                } 
            }
        }
    } 
}