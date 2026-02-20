using System;
using FactorySystem.Core;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    [Serializable]
    public class FruitMerchant : Factory<Fruit>
    {
        private FactoryItemData itemData;
        
        public FruitMerchant(FactoryItemData data)
        {
            itemData = data;
        }

        protected override Fruit CreateItem()
        {
            return new Fruit(itemData);
        }

        protected override FactoryItemData GetProductData()
        {
            return itemData;
        }
    }
}