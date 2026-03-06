using System;
using System.Collections;
using System.Collections.Generic;
using FactorySystem.Core.Doors;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Core
{
    // Factory.cs
// On ajoute IItemOutput<T> à la liste des interfaces implémentées.
// Cela force Factory à avoir les Outputs, AddOutput, RemoveOutput et SendItem.

    [Serializable]
    public abstract class Factory<T> : IFactory, IItemProducer, IItemOutput<T> where T : IFactoryItem
    {
        public event Action<T> OnItemWaiting;
        public event Action<T> OnItemRemoved;
        public event Action<T> OnItemDispatched;

        public float RemainingTimeUntilNextProduct { get; private set; }
        [field: SerializeField]
        public int MaxItemQuantity { get; protected set; } = 1;
        
        private float ProductionDuration = 5f;
        
        public List<IFactoryItem> ItemsList { get; } = new List<IFactoryItem>();

        //List avec les Outputs
        public List<IItemInput<T>> Outputs { get; } = new List<IItemInput<T>>();
        
        private Queue<T> waitingItems = new Queue<T>();
        
        public void SetParameters(int maxItemQuantity, float productionDuration)
        {
            MaxItemQuantity = maxItemQuantity;
            ProductionDuration = productionDuration;
        }
        
        
        public void Initialize()
        {
            RemainingTimeUntilNextProduct = ProductionDuration;
        }
        
        
        public void AddOutput(IItemInput<T> input)
        {
            if (!Outputs.Contains(input))
                Outputs.Add(input);
        }
        public void RemoveOutput(IItemInput<T> input)
        {
            Outputs.Remove(input);
        }
        
        public void SendItem(T item)
        {
            foreach (IItemInput<T> output in Outputs)
            {
                output.ReceiveItem(item);
            }
        }
        public void TryDispatchNextWaitingItem()
        {
            if (waitingItems.Count == 0)
                return;

            foreach (IItemInput<T> output in Outputs)
            {
                if (output.CanReceiveItem)
                {
                    T nextItem = waitingItems.Dequeue();
            
                    // On NE retire PAS de ItemsList ici
                    // L'item est encore en transit vers le Storage
                    // ItemsList.Remove(nextItem) ← SUPPRIME CETTE LIGNE
            
                    SendItem(nextItem);
                    OnItemDispatched?.Invoke(nextItem);
                    return;
                }
            }
        }
        
        public virtual void UpdateFactory(float elapsedTime)
        {
            RemainingTimeUntilNextProduct -= elapsedTime * ProductionDuration;
            if (RemainingTimeUntilNextProduct > 0)
                return;

            RemainingTimeUntilNextProduct = ProductionDuration;

            if (ItemsList.Count >= MaxItemQuantity)
                return;
            
            T item = CreateItem();
            ItemsList.Add(item);

            if (!TrySendItem(item))
            {
                // L'item ne peut pas partir, il attend dans la factory
                waitingItems.Enqueue(item);
                OnItemWaiting?.Invoke(item); // on affiche dans le grid
            }
        }

        private bool TrySendItem(T item)
        {
            // S'il y a des items en attente, on respecte la file
            if (waitingItems.Count > 0)
                return false;

            // On vérifie si au moins un output peut recevoir
            foreach (IItemInput<T> output in Outputs)
            {
                if (output.CanReceiveItem)
                {
                    SendItem(item);
                    return true;
                }
            }

            // Aucun output disponible, l'item reste en attente
            return false;
        }

        
        public void RemoveItem(T item)
        {
            ItemsList.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        protected abstract T CreateItem();
        protected abstract FactoryItemData GetProductData();
    }
}