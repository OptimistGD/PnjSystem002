using System.Collections.Generic;
using PNJSystem.Core.Items;
using PNJSystem.Core.Utilities;
using UnityEngine;

namespace PNJSystem.Core
{
    public class PnjManager
    {
        private readonly IProfession profession;
        private readonly PnjInventory inventory;
        
        
        public PnjManager(IProfession profession, PnjInventory inventory)
        {
            this.profession = profession;
            this.inventory = inventory;
        }
        
        public IReadOnlyList<Item> GetItems()
        {
            return inventory.Items;
        }
        
        public object GiveItem(string itemDescription)
        {
            return inventory.TakeItem(itemDescription);
        }
    }
}
