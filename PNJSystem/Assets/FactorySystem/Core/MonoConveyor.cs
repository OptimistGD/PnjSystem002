using System;
using System.Collections.Generic;
using FactorySystem.Core;
using FactorySystem.Core.Items;
using UnityEngine;

public class MonoConveyor : MonoBehaviour
{
    private List<IFactoryItem> itemsList;
    /*
    public void OnEnable(IItemProducer Producer)
    {
        Producer.OnItemProduced += OnItemReceived;
    }

    public void OnDisable(IItemProducer Producer)
    {
        Producer.OnItemProduced -= OnItemReceived;
    }
    
    private void OnItemReceived(IFactoryItem item)
    {
        itemsList.Add(item);
        Conveyor.ItemOnConveyor();
        // ton système de déplacement ici
    }
    */
}
