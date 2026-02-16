using UnityEngine;

namespace FactorySystem.Core.Items
{
    public interface IFactoryItem
    {
        public FactoryItemData Data { get; }
    }
}