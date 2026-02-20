using System;
using FactorySystem.Core;
using FactorySystem.Core.Items;
using TMPro;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    public class MonoFruitMerchant : MonoFactory<Fruit>
    {
        [SerializeField]
        private FactoryItemData itemData;

        [SerializeField] 
        private TMP_Text fruitName;

        protected override Factory<Fruit> CreateFactory()
        {
            // On vérifie que itemData est assigné dans l'Inspector
            if (itemData == null)
            {
                Debug.LogError("[MonoFruitMerchant] itemData non assigné dans l'Inspector !");
                return null;
            }

            // On passe itemData au constructeur de FruitMerchant
            return new FruitMerchant(itemData);
        }
    }
}