using System;
using System.Collections.Generic;
using FactorySystem.Core.Doors;
using FactorySystem.Core.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace FactorySystem.Core
{
    //Mono = View = lien entre logic et UI
    //abstraite et générique en T => ? type d'item
    public abstract class MonoFactory<T> : MonoBehaviour where T : IFactoryItem
    {
        // [SerializeReference] => Unity a besoin de coco le vrai factory
        [SerializeReference]
        private Factory<T> factory;
        
        private FactoryManager factoryManager;
        
        [SerializeField]
        private List<MonoBehaviour> outputObjects;
        
        protected Factory<T> FactoryLogic => factory;

        private void Awake()
        {
            if (factory == null)
            {
                factory = CreateFactory();
            }
            factory.Initialize();
            
            ConnectOutputs();
        }

        private void Update()
        {
            if (factory == null)
            {
                Debug.LogError("[MonoFactory] _factory est null ! Assigne-la dans l'Inspector ou implémente CreateFactory().");
                return;
            }
            
            factory.UpdateFactory(Time.deltaTime);
        }
        
        protected abstract Factory<T> CreateFactory();
        
        private void ConnectOutputs()
        {
            foreach (MonoBehaviour obj in outputObjects)
            {
                // "as" tente une conversion : si l'objet n'est pas un IItemInput<T>,
                // la variable "input" vaudra null et on ignore cet objet.
                IItemInput<T> input = obj as IItemInput<T>;

                if (input != null)
                {
                    factory.AddOutput(input);
                }
                else
                {
                    Debug.LogWarning($"[MonoFactory] {obj.name} n'implémente pas IItemInput<T>. Ignoré.");
                }
            }
        }
        //connecter
        public void AddOutput(MonoBehaviour obj)
        {
            IItemInput<T> input = obj as IItemInput<T>;
            if (input != null)
                factory.AddOutput(input);
        }
        //déconnecter
        public void RemoveOutput(MonoBehaviour obj)
        {
            IItemInput<T> input = obj as IItemInput<T>;
            if (input != null)
                factory.RemoveOutput(input);
        }
    }
}