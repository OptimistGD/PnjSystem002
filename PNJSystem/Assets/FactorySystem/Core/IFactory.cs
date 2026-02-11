using FactorySystem.Core.Items;

namespace FactorySystem.Core
{
    public interface IFactory
    {
        float Efficiency { get; }
        int MaxItemQuantity { get; }
        void UpdateFactory(float elapsedTime);
    }
}