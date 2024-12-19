using Data.Inventory;
using Tools;
using UnityEngine;

namespace Data
{
    public class DataCentralService : MonoBehaviour
    {
        [SerializeField] private AddSpriteForItem _addSpriteForItem;
        private JsonSerialization _jsonSerialization = new();
        private string _pathInventory;

        public static DataCentralService Instance;
        public InventoryStates InventoryStates = new();
         
        private void Awake()
        {
            Instance = this;
            _pathInventory = _jsonSerialization.CreatePath($"{InventoryStates}");
            LoadStates();
        }

        public void SaveStates()
        {
            _jsonSerialization.WriteToTextFile(JsonUtility.ToJson(InventoryStates.GetStates()), _pathInventory);
        }

        public void LoadStates()
        {
            InventoryStates.ReadStates(_jsonSerialization.DeSerialization(_pathInventory));
            _addSpriteForItem.GiveSpriteItem();
        }
    }
}