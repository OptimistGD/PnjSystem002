using FactorySystem.Core;
using FactorySystem.Core.Items;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    public class MonoFactoryFruit : MonoFactory<Fruit>
    {
        //view des shops
        //barre de progression, etc
        
        [SerializeField]
        private FactoryItemData itemData;

        protected override Factory<Fruit> CreateFactory()
        {
            // On vérifie que itemData est bien assigné avant de créer la factory.
            if (itemData == null)
            {
                Debug.LogError("[MonoFactoryFruit] itemData non assigné dans l'Inspector !");
                return null;
            }

            return new FruitMerchant(itemData);
        }
    }
}