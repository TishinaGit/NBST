using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Inventory
{
    public class InventoryStates
    {
        private Dictionary<int, InventoryCellData> InventoryCells = new();

        public InventoryCellData GetCellData(int index)
        {
            if (InventoryCells.ContainsKey(index))
            {
                return InventoryCells[index];
            }

            var cellJson = new InventoryCellData()
            {
                CellIndex = index,
            };
            InventoryCells.Add(index, cellJson);

            return cellJson;
        }

        public void UpdateCellData(InventoryCellData data)
        {
            InventoryCells[data.CellIndex] = data;
        }

        public void ReadStates(string json)
        {
            if (string.IsNullOrEmpty(json))
                SetAndInitEmptyData(new InventoryStatesJson());
            else
                SetData(JsonUtility.FromJson<InventoryStatesJson>(json));
        }

        public object GetStates()
        {
            return new InventoryStatesJson
            {
                Cells = InventoryCells.Values.ToList(),
            };
        }

        private void SetData(InventoryStatesJson mapStagesData)
        {
            InventoryCells.Clear();
            mapStagesData.Cells ??= new List<InventoryCellData>();

            ConvertJsonCellsToCells(mapStagesData.Cells);
        }


        private void SetAndInitEmptyData(InventoryStatesJson mapStagesData)
        {
            SetData(mapStagesData);
        }

        private void ConvertJsonCellsToCells(List<InventoryCellData> json)
        {
            foreach (var cellJson in json)
            {
                InventoryCells.Add(cellJson.CellIndex, cellJson);
            }
        }
    }
}