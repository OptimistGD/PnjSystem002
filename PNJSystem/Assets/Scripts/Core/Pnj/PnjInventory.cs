using System.Collections.Generic;
using System.Linq;
using PNJSystem.Core.Items;

namespace PNJSystem.Core
{
    public class PnjInventory
    {
        //liste des items 
        private readonly List<Item> items;
        public IReadOnlyList<Item> Items => items;
        
        
        public PnjInventory(IEnumerable<Item> items)
        {
            this.items = new List<Item>(items);
        }
        
        public Item TakeItem(string itemDescription)
        {
            var item = items.FirstOrDefault(i => i.Id == itemDescription);
            if (item == null)
            {
                return null;
            }
            
            items.Remove(item);
            return item;
        }
    }
}