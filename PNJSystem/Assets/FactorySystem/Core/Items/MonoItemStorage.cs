using System.Collections.Generic;
using FactorySystem.Core.Doors;
using UnityEngine;

namespace FactorySystem.Core.Items
{
    //IItemInput => espace où item peuvent entrer 
    public abstract class MonoItemStorage<T> : MonoBehaviour, IItemInput<T> where T : IFactoryItem
    {
        [SerializeReference] private ItemStorage<T> storage = new ItemStorage<T>();
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private RectTransform container;
        
        [SerializeField]
        private List<MonoBehaviour> connectedFactories;
        public ItemStorage<T> Storage => storage;
        public bool CanReceiveItem => !storage.IsFull;

        // Dictionnaire qui lie chaque item logique à son GameObject UI.
        // Cela nous permet de retrouver et supprimer le bon visuel
        // quand un item est retiré.
        private Dictionary<T, GameObject> itemInView = new Dictionary<T, GameObject>();

        private void Awake()
        {
            storage.OnItemAdded += OnItemAdded;
            storage.OnItemRemoved += OnItemRemoved;
            storage.OnSlotFree += OnSlotFree;
        }
        private void OnDestroy()
        {
            storage.OnItemAdded -= OnItemAdded;
            storage.OnItemRemoved -= OnItemRemoved;
            storage.OnSlotFree += OnSlotFree;
        }
        
        
        public void ReceiveItem(T item)
        {
            storage.TryAddItem(item);
        }
        
        
        private void OnItemAdded(T item)
        {
            if (itemPrefab == null || container == null)
            {
                Debug.LogWarning("[MonoItemStorage] ItemUIPrefab ou LayoutContainer non assigné !");
                return;
            }

            //Instantiate(quoi instancier, où instancier)
            GameObject itemView = Instantiate(itemPrefab, container);
            
            itemInView[item] = itemView;
            SetupItemUI(item, itemView);
        }
        private void OnItemRemoved(T item)
        {
            if (itemInView.TryGetValue(item, out GameObject itemView))
            {
                Destroy(itemView);
                itemInView.Remove(item);
            }
        }
        private void OnSlotFree()
        {
            foreach (MonoBehaviour obj in connectedFactories)
            {
                // On tente de caster en Factory<T> pour appeler TryDispatchNextWaitingItem
                // "is" vérifie le type sans planter si ça ne correspond pas
                if (obj is MonoFactory<T> monoFactory)
                {
                    monoFactory.FactoryLogic.TryDispatchNextWaitingItem();
                }
                else
                {
                    Debug.LogWarning($"[MonoItemStorage] {obj.name} n'est pas un MonoFactory<T>. Ignoré.");
                }
            }
        }
        
        

        // Méthode abstraite => enfant gère propre SetupItemUI
        protected abstract void SetupItemUI(T item, GameObject itemView);
    }
}