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
        public event Action<T> OnItemCreated;
        public event Action<T> OnItemRemoved;

        [field: SerializeField] public float Efficiency { get; protected set; } = 5;
        [field: SerializeField] public int MaxItemQuantity { get; protected set; } = 5;

        public float RemainingTimeUntilNextProduct { get; private set; }
        public List<IFactoryItem> ItemsList { get; } = new List<IFactoryItem>();

        //List avec les Outputs
        public List<IItemInput<T>> Outputs { get; } = new List<IItemInput<T>>();
        
        
        public void Initialize()
        {
            RemainingTimeUntilNextProduct = GetProductData().ProductionDuration;
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
        

        public virtual void UpdateFactory(float elapsedTime)
        {
            RemainingTimeUntilNextProduct -= elapsedTime * Efficiency;
            
            if (RemainingTimeUntilNextProduct > 0)
                return;

            RemainingTimeUntilNextProduct = 0;
            if (ItemsList.Count >= MaxItemQuantity)
                return;

            RemainingTimeUntilNextProduct = GetProductData().ProductionDuration;

            T item = CreateItem();
            ItemsList.Add(item);

            OnItemCreated?.Invoke(item);

            // NOUVEAU : dès qu'un item est créé, on l'envoie vers toutes les sorties connectées
            SendItem(item);
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