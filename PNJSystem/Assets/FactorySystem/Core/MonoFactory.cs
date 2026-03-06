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
        [SerializeField]
        private List<MonoBehaviour> outputObjects;
        [SerializeField]
        private RectTransform waitingLayoutContainer;
        [SerializeField]
        private GameObject itemUIPrefab;
        
        [SerializeField, Min(1)] private int maxItemQuantity = 1;
        [SerializeField, Min(0.1f)] private float productionDuration = 5f;
        
        private FactoryManager factoryManager;

        public Factory<T> FactoryLogic => factory;
        
        private Dictionary<T, GameObject> waitingUIObjects = new Dictionary<T, GameObject>();

        private void Awake()
        {
            if (factory == null)
            {
                factory = CreateFactory();
            }
            factory.SetParameters(maxItemQuantity, productionDuration);
            factory.Initialize();
            
            factory.OnItemWaiting += OnItemWaiting;
            factory.OnItemDispatched += OnItemDispatched;
            
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
        
        /*
        private void OnItemCreated(T item)
        {
            if (itemUIPrefab == null || waitingLayoutContainer == null)
                return;

            GameObject uiObject = Instantiate(itemUIPrefab, waitingLayoutContainer);
            waitingUIObjects[item] = uiObject;
            SetupItemUI(item, uiObject);
        }
        */
        private void OnItemWaiting(T item)
        {
            if (itemUIPrefab == null || waitingLayoutContainer == null)
                return;

            GameObject uiObject = Instantiate(itemUIPrefab, waitingLayoutContainer);
            waitingUIObjects[item] = uiObject;
            SetupItemUI(item, uiObject);
        }
        // NOUVEAU : quand un item part vers le conveyor, on retire son visuel
        private void OnItemDispatched(T item)
        {
            if (waitingUIObjects.TryGetValue(item, out GameObject uiObject))
            {
                Destroy(uiObject);
                waitingUIObjects.Remove(item);
            }
        }
        
        protected virtual void SetupItemUI(T item, GameObject uiObject) { }
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