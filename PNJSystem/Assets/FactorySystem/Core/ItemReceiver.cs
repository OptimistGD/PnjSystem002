using FactorySystem.Core.Doors;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Core
{
    public abstract class ItemReceiver<T> : MonoBehaviour, IItemInput<T> where T : IFactoryItem
    {
        protected int TotalItemsReceived { get; private set; }
        
        public void ReceiveItem(T item)
        {
            TotalItemsReceived++;

            Debug.Log($"[ItemReceiver] Item reçu ! Total : {TotalItemsReceived}");
            
            OnItemReceived(item);
        }
        
        protected abstract void OnItemReceived(T item);
    }
}