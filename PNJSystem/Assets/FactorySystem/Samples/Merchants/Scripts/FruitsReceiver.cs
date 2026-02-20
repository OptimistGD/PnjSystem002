using System.Collections.Generic;
using FactorySystem.Core;
using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    public class FruitsReceiver : ItemReceiver<Fruit>
    {
        // Liste de tous les items accumulés dans ce receiver.
        private List<Fruit> storedItems = new List<Fruit>();

        protected override void OnItemReceived(Fruit item)
        {
            storedItems.Add(item);
            Debug.Log($"[TestItemReceiver] Fruit stocké ! Stock actuel : {storedItems.Count}");
        }
    }
}