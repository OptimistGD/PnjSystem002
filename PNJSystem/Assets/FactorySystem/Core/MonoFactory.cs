using System;
using System.Collections.Generic;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Core
{
    public abstract class MonoFactory<T> : MonoBehaviour 
        where T : IFactoryItem
    {
        public Factory<T> Factory { get; protected set; }

        [SerializeField]
        private Transform container;
        [SerializeField]
        private MonoFactoryItem<T> factoryItemPrefab;

        private Dictionary<T, MonoFactoryItem<T>> items = new();
        
        private void Awake()
        {
            Factory =  CreateFactory();
        }

        private void OnEnable()
        {
            FactoryManager.Instance.AddFactory(Factory);
            
            Factory.OnItemCreated += FactoryOnItemCreated;
            Factory.OnItemRemoved += FactoryOnItemRemoved;
        }
        
        private void OnDisable()
        {
            FactoryManager.Instance.RemoveFactory(Factory);
            
            Factory.OnItemCreated -= FactoryOnItemCreated;
            Factory.OnItemRemoved -= FactoryOnItemRemoved;
        }

        protected abstract Factory<T> CreateFactory();
        
        private void FactoryOnItemCreated(T item)
        {
            if(items.ContainsKey(item))
                return;
            
            MonoFactoryItem<T> instance = Instantiate(factoryItemPrefab, container);
            instance.Connect(item);
            
            items.Add(item, instance);
        }
        
        private void FactoryOnItemRemoved(T item)
        {
            if (items.TryGetValue(item, out MonoFactoryItem<T> instance))
            {
                items.Remove(item);
                instance.Disconnect(item);
                
                Destroy(instance.gameObject);
            }
        }
    }
}