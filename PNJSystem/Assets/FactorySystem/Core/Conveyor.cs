using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Conveyor : MonoBehaviour
{
    //propre aux items => à déplacer et communiquer 
    [System.Serializable]
    public class ConveyorItem
    {
        public Transform item;
        [HideInInspector] public float currentLerp;
        [HideInInspector] public int startPoint;
    }
    [SerializeField] private List<ConveyorItem> itemsList;
    
    [SerializeField] private float itemSpacing;
    [SerializeField] private float speed;
    [SerializeField] private LineRenderer lineRenderer;

    //plus opti sinon trop items à vérifier 
    private void FixedUpdate()
    {
        for (int i = 0; i < itemsList.Count ; i++)
        {
            ConveyorItem conveyorItem = itemsList[i];
            Transform item = itemsList[i].item;

            if (i > 0)
            {
                if (Vector3.Distance(item.position, itemsList[i - 1].item.position) <= itemSpacing)
                {
                    continue;
                }
            }
            
            item.transform.position = Vector3.Lerp(lineRenderer.GetPosition(conveyorItem.startPoint), lineRenderer.GetPosition(conveyorItem.startPoint + 1), conveyorItem.currentLerp);
            float distance = Vector3.Distance(lineRenderer.GetPosition(conveyorItem.startPoint), lineRenderer.GetPosition(conveyorItem.startPoint + 1));
            conveyorItem.currentLerp += (speed * Time.deltaTime) /  distance;

            if (conveyorItem.currentLerp >= 1)
            {
                if (conveyorItem.startPoint + 2 < lineRenderer.positionCount)
                {
                    conveyorItem.currentLerp = 0;
                    conveyorItem.startPoint++;
                }
                else
                {
                    //fin de conveyor
                    //ici bac à fruit
                    //event ? 
                }
            }
        }
    }
}
