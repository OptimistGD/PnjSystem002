using FactorySystem.Core.Items;
using UnityEngine;
using UnityEngine.UI;

namespace FactorySystem.Samples.Merchants
{
    
    public class MonoFruitStorage : MonoItemStorage<Fruit>
    {
        protected override void SetupItemUI(Fruit item, GameObject itemView)
        {
            SpriteRenderer icon = itemView.GetComponentInChildren<SpriteRenderer>();

            if (icon != null)
                icon.sprite = item.Data.Icon;
            else
                Debug.LogWarning("[MonoFruitStorage] Aucun composant Image trouvé sur le prefab UI !");
            
        }
    
    }
 
}