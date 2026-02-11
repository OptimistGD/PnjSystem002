using UnityEngine;

namespace PNJSystem.Core.Items
{
    [CreateAssetMenu(menuName = "Items/Item")]
    public class ItemData : ScriptableObject
    {
        public string id;
        public string description;
        
        //si je fais ça, Data (logic) parle avec UI => problème ? 
        public Sprite icon;
    }
}