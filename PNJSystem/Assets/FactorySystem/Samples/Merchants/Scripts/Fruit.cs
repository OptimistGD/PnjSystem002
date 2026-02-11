using FactorySystem.Core.Items;

namespace FactorySystem.Samples.Merchants
{
    public class Fruit : IFactoryItem
    {
        public FactoryItemData Data { get; }
        
        public Fruit(FactoryItemData data)
        {
            Data = data;
        }

    }
}