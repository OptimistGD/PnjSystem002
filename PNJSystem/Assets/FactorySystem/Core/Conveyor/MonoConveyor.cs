using System;
using System.Collections.Generic;
using FactorySystem.Core;
using FactorySystem.Core.Items;
using UnityEngine;

public class MonoConveyor : ConveyorBelt<IFactoryItem>
{
    private List<IFactoryItem> itemsList;

    [SerializeField]
    private Transform endPoint;

    protected override Vector3 GetEndPoint()
    {
        return endPoint.position;
    }
}
