using System;
using System.Collections.Generic;
using FactorySystem.Core.Items;
using UnityEngine;
using UnityEngine.Pool;

namespace FactorySystem.Core
{
    public class FactoryManager : MonoBehaviour
    {
        //singleton
        public static FactoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("FactoryManager");
                    instance = gameObject.AddComponent<FactoryManager>();
                    
                    DontDestroyOnLoad(instance);
                }
                
                return instance;
            }
        }

        private static FactoryManager instance;
        
        private List<IFactory> activeFactories = new List<IFactory>();

        public void AddFactory<T>(Factory<T> factory) where T : IFactoryItem
        {
            activeFactories.Add(factory);
        }

        public void RemoveFactory<T>(Factory<T> factory) where T : IFactoryItem
        {
            activeFactories.Remove(factory);
        }

        private void Update()
        {
            float elapsedTime = Time.deltaTime;
            using (ListPool<IFactory>.Get(out List<IFactory> factoryPool))
            {
                factoryPool.AddRange(activeFactories);
                foreach (IFactory factory in factoryPool)
                {
                    factory.UpdateFactory(elapsedTime);
                }
            }
        }
    }
}