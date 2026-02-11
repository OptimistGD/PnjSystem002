using System;
using FactorySystem.Core;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    [Serializable]
    public class FruitMerchant : Factory<Fruit>
    {
        [field: SerializeField]
        public FruitData FruitData { get; private set; }
        
        protected override Fruit CreateItem()
        {
            return new Fruit(FruitData);
        }

        protected override FactoryItemData GetProductData()
        {
            return FruitData;
        }
    }
}