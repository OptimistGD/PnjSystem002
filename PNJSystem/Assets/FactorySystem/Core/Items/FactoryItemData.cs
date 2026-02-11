using UnityEngine;

namespace FactorySystem.Core.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "FactorySystem/Item")]
    public class FactoryItemData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField, Min(0)]
        public float ProductionDuration { get; private set; }
    }
}