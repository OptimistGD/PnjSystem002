using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactorySystem.Core.Items
{
    public class ItemStorage<T> where T : IFactoryItem
    {
        [SerializeField]
        private int maxSlots = 5;
        public int Count => itemsInStorage.Count;
        public int MaxSlots => maxSlots;
        public bool IsFull => itemsInStorage.Count >= maxSlots;
        
        private List<T> itemsInStorage = new List<T>();
        // Propriété publique en lecture seule pour accéder aux items depuis l'extérieur.
        public IReadOnlyList<T> ItemsInStorage => itemsInStorage;
        

        //event => ajout
        public event Action<T> OnItemAdded;
        //event => remove
        public event Action<T> OnItemRemoved;
        
        
        public bool TryAddItem(T item)
        {
            if (IsFull)
            {
                Debug.LogWarning("[ItemStorage] Stockage plein !");
                return false;
            }

            itemsInStorage.Add(item);
            OnItemAdded?.Invoke(item);
            return true;
        }
        
        public bool TryRemoveItem(T item)
        {
            if (!itemsInStorage.Contains(item))
                return false;

            itemsInStorage.Remove(item);
            OnItemRemoved?.Invoke(item);
            return true;
        }
    }
}