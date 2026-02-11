using System.Collections.Generic;
using PNJSystem.Core.Items;
using UnityEngine;

namespace PNJSystem.Core.Inventory
{
    public class PlayerInventory
    {
        public static PlayerInventory Instance { get; } = new PlayerInventory();
        private readonly List<Item> items = new();

        //récupère les items des PNJ
        //MANQUE => voir les objets pris
        public void Add(Item item)
        {
            items.Add(item);
            Debug.Log($"Objet récupéré : {item.Name}");
        }
    }
}