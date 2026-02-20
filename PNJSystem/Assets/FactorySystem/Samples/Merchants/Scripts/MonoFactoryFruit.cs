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
        private FactoryItemData _itemData;

        protected override Factory<Fruit> CreateFactory()
        {
            // On vérifie que _itemData est bien assigné avant de créer la factory.
            if (_itemData == null)
            {
                Debug.LogError("[MonoTestFactory] _itemData non assigné dans l'Inspector !");
                return null;
            }

            return new FruitMerchant(_itemData);
        }
    }
}