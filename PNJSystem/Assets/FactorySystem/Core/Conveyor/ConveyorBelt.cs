using System;
using System.Collections;
using System.Collections.Generic;
using FactorySystem.Core.Doors;
using FactorySystem.Core.Items;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ConveyorBelt<T> : MonoBehaviour, IItemInput<T>, IItemOutput<T> where T : IFactoryItem
{
    [SerializeField] private GameObject itemPrefab;
    
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private float speed = 1f;
    
    public List<IItemInput<T>> Outputs { get; } = new List<IItemInput<T>>();
    
    private List<(GameObject obj, Vector3 destination, T item)> itemsInTransit = new();
    
    
    public void ReceiveItem(T item)
    {
        // || => ou
        if (itemPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("[ConveyorBelt] ItemPrefab ou SpawnPoint non assigné !");
            return;
        }
        
        GameObject spawnedObject = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        
        itemsInTransit.Add((spawnedObject, GetEndPoint(), item));
        
        OnItemReceived(item, spawnedObject);
    }
    
    
    public void AddOutput(IItemInput<T> input)
    {
        if (!Outputs.Contains(input))
            Outputs.Add(input);
    }
    public void RemoveOutput(IItemInput<T> input)
    {
        Outputs.Remove(input);
    }

    public void SendItem(T item)
    {
        foreach (IItemInput<T> output in Outputs)
            output.ReceiveItem(item);
    }
    

    private void Update()
    {
        MoveItems();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void MoveItems()
    {
        List<(GameObject, Vector3, T)> arrived = new();

        // On stocke aussi le item logique avec le GameObject
        // pour pouvoir l'envoyer aux outputs à l'arrivée.
        for (int i = 0; i < itemsInTransit.Count; i++)
        {
            var (obj, destination, item) = itemsInTransit[i];

            obj.transform.position = Vector3.MoveTowards(
                obj.transform.position,
                destination,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(obj.transform.position, destination) < 0.01f)
                arrived.Add((obj, destination, item));
        }

        foreach (var (obj, destination, item) in arrived)
        {
            itemsInTransit.Remove((obj, destination, item));
            OnItemArrived(obj);
            SendItem(item);
            if (Outputs.Count == 0)
                Destroy(obj);
        }
    }
    
    protected abstract Vector3 GetEndPoint();

    protected virtual void OnItemReceived(T item, GameObject spawnedObject) { }

    protected virtual void OnItemArrived(GameObject obj)
    {
        if (Outputs.Count == 0)
            Destroy(obj);
    }
}