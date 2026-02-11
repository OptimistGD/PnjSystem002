using System.Linq;
using PNJSystem.Core.Items;
using PNJSystem.Core.Utilities;
using UnityEngine;

namespace PNJSystem.Core
{
    //assigne une data profession au PNJ et ces items
    public static class PnjFactory
    {
        public static PnjController Create(ProfessionData data)
        {
            //Debug.Log($"[Factory] Profession : {data.description}");
            //Debug.Log($"[Factory] StartingItems COUNT = {data.startingItems?.Count}");

            //en cas de debug
            /*
            if (data.startingItems == null || data.startingItems.Count == 0)
            {
                //Debug.LogWarning("[Factory] AUCUN ITEM DANS LA PROFESSION");
            }
            */

            var items = data.startingItems.Select(i =>
            {
                //Debug.Log($"[Factory] ItemData → {i.id} / {i.description}");
                return new Item(i.id, i.description);
            });

            return new PnjController(new Profession(data.id, data.description), new PnjInventory(items));
        }
    }
}