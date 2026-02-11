using System.Collections.Generic;
using PNJSystem.Core.Items;
using UnityEngine;

namespace PNJSystem.Core.Utilities
{
    //scriptableObject pour les datas de Unity
    [CreateAssetMenu(menuName = "PNJ/Profession")]
    public class ProfessionData : ScriptableObject
    {
        public ProfessionId id;
        public string description;
        
        //si je fais ça, Data (logic) parle avec UI => problème ?
        public Sprite icon;

        public List<ItemData> startingItems;
    }
}