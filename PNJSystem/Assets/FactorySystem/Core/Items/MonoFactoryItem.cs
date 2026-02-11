using UnityEngine;

namespace FactorySystem.Core.Items
{
    //Mono = View(sign & feedback) = UI
    public abstract class MonoFactoryItem<T> : MonoBehaviour where T : IFactoryItem
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public virtual void Connect(T item)
        {
            spriteRenderer.sprite = item.Data.Icon;
        }
        
        public virtual void Disconnect(T item)
        {
            spriteRenderer.sprite = null;
        }
    }
}