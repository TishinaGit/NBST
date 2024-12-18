using System.Collections.Generic;
using UnityEngine;

using ReceiptsSystem;

namespace CraftSystem
{
    [CreateAssetMenu(fileName = "ReceptPotions", menuName = "ReceptPotions")]
    public class CraftSO : ScriptableObject
    {
        public List<Receipts> ReceiptsPotions;
    }
}
