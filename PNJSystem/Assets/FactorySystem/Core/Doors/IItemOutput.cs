using System.Collections.Generic;
using FactorySystem.Core.Items;

namespace FactorySystem.Core.Doors
{
    // Le "where T : IFactoryItem" signifie :"T peut être n'importe quel type, MAIS il doit implémenter IFactoryItem"
    public interface IItemOutput<T> where T : IFactoryItem
    {
        // Liste des sorties connectées à cet objet.
        List<IItemInput<T>> Outputs { get; }

        //connecter depuis l'extérieur.
        void AddOutput(IItemInput<T> input);

        //déconnecter
        void RemoveOutput(IItemInput<T> input);

        //item prêt à être envoyé.
        void SendItem(T item);
        
    }
}