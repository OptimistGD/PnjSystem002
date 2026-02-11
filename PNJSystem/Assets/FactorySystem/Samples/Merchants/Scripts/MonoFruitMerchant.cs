using System;
using FactorySystem.Core;
using TMPro;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    public class MonoFruitMerchant : MonoFactory<Fruit>
    {
        [SerializeField] 
        private FruitMerchant merchant;

        [SerializeField] private TMP_Text fruitName;

        private void Start()
        {
            fruitName.text = merchant.FruitData.Title;
        }

        protected override Factory<Fruit> CreateFactory()
        {
            return merchant;
        }
    }
}