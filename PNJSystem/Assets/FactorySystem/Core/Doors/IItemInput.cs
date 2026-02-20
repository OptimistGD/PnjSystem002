using FactorySystem.Core.Items;

namespace FactorySystem.Core.Doors
{
    public interface IItemInput<T> where T : IFactoryItem
    {
        // IItemInput.cs
        //envoie un item à cet objet.
        //ConveyorBelt => instancier l'item
        void ReceiveItem(T item);
    }
}