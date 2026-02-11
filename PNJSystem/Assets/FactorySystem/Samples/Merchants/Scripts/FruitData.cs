using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    [CreateAssetMenu(fileName = "New Item", menuName = "FactorySystem/Samples/Fruit")]
    public class FruitData : FactoryItemData
    {
        [field: SerializeField]
        public int SellPrice { get; private set; }
    }
}