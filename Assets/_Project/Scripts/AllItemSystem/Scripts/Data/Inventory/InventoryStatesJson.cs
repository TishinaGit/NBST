using System;
using System.Collections.Generic;

namespace Data.Inventory
{
    [Serializable]
    public struct InventoryStatesJson
    {
        public List<InventoryCellData> Cells;
    }
}