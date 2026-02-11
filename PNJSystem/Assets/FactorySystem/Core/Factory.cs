using System;
using System.Collections;
using System.Collections.Generic;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Core
{
    [Serializable]
    public abstract class Factory<T> : IFactory where T : IFactoryItem
    {
        public event Action<T> OnItemCreated;
        public event Action<T> OnItemRemoved;
        
        [field: SerializeField]
        public float Efficiency { get; protected set; } = 1;
        
        [field: SerializeField]
        public int MaxItemQuantity { get; protected set; } = 1;
        
        public float RemainingTimeUntilNextProduct { get; private set; }
        
        private List<T> createdItems = new List<T>();

        public virtual void UpdateFactory(float elapsedTime)
        {
            RemainingTimeUntilNextProduct -= elapsedTime * Efficiency;
            if (RemainingTimeUntilNextProduct > 0)
                return;

            RemainingTimeUntilNextProduct = 0;
            if (createdItems.Count >= MaxItemQuantity)
                return;
            
            RemainingTimeUntilNextProduct = GetProductData().ProductionDuration;

            T item = CreateItem();
            createdItems.Add(item);
            
            OnItemCreated?.Invoke(item);
        }

        public void RemoveItem(T item)
        {
            createdItems.Remove(item);
            OnItemRemoved?.Invoke(item);
        }
        
        protected abstract T CreateItem();
        protected abstract FactoryItemData GetProductData();
    }
}