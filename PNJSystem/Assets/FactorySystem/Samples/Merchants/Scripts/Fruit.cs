using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    public class Fruit : IFactoryItem
    {
        //element Factory
        public FactoryItemData Data { get; }

        public Fruit(FactoryItemData data)
        {
            Data = data;
        }
        

    }
}