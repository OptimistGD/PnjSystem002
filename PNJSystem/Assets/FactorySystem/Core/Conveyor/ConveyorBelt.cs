using System;
using System.Collections;
using System.Collections.Generic;
using FactorySystem.Core.Doors;
using FactorySystem.Core.Items;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ConveyorBelt<T> : MonoBehaviour, IItemInput<T>, IItemOutput<T> where T : IFactoryItem
{
    private struct ItemsInTransit
    {
        public GameObject Obj;
        
        public Vector3 CurrentPosition;
        public Vector3 Destination;
        public float DistanceBetween;
        
        public T Item;
    }
    
    
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float speed = 1f;
    
    [SerializeField]
    private List<MonoBehaviour> outputObjects;
    public List<IItemInput<T>> Outputs { get; } = new List<IItemInput<T>>();

    private List<ItemsInTransit> itemsInTransit = new();
    private List<ItemsInTransit> itemsArrived = new();

    private void Awake()
    {
        ConnectOutputs();
    }
    private void ConnectOutputs()
    {
        foreach (MonoBehaviour obj in outputObjects)
        {
            if (obj is IItemInput<T> input)
                Outputs.Add(input);
            else
                Debug.LogWarning($"[ConveyorBelt] {obj.name} n'implémente pas IItemInput<T>. Ignoré.");
        }
    }
    
    public void ReceiveItem(T item)
    {
        // || => ou
        if (itemPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("[ConveyorBelt] ItemPrefab ou SpawnPoint non assigné !");
            return;
        }
        
        GameObject spawnedObject = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        
        //itemsInTransit.Add((spawnedObject, GetEndPoint(), item));
        itemsInTransit.Add(new ItemsInTransit());
        
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
    public bool CanReceiveItem
    {
        get
        {
            if (Outputs.Count == 0)
                return true;

            // On vérifie que tous les outputs peuvent recevoir
            foreach (IItemInput<T> output in Outputs)
            {
                if (output.CanReceiveItem)
                    return true;
            }
            return false;
        }
    }
    

    private void Update()
    {
        MoveItems();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void MoveItems()
    {
        //int = pas changer => INITIALISATION
        // i = index 
        // Count = Length => max de la liste 
        //i++ => rajout de 1
        for (var i = 0; i < itemsInTransit.Count; i++)
        {
            // var => définit temporairement la liste des i
            ItemsInTransit itemsMoving = itemsInTransit[i];
            
            
            //Obj = GameObject => récupère posiiton Vector3 du GameObject (.transform.position)
            itemsMoving.Obj.transform.position = Vector3.MoveTowards(itemsMoving.CurrentPosition,
                itemsMoving.Destination,
                speed * Time.deltaTime);

            itemsMoving.DistanceBetween = (itemsMoving.CurrentPosition - itemsMoving.Destination).sqrMagnitude;
            //comme Distance = valeur au carré, alors, sqrMagnitude => calcul sans carré
            if (itemsMoving.DistanceBetween < 0.01f)
            {
                itemsArrived.Add(itemsMoving);
                SendItem(itemsMoving.Item);


                OnItemArrived(itemsMoving.Obj);
                Destroy(itemsMoving.Obj);
            }
            
            itemsInTransit[i] = itemsMoving;
        }
        
        itemsInTransit.RemoveAll(ctx => ctx.DistanceBetween < 0.01f);
    }
    
    protected abstract Vector3 GetEndPoint();

    protected virtual void OnItemReceived(T item, GameObject spawnedObject) { }

    protected virtual void OnItemArrived(GameObject obj)
    {
        if (Outputs.Count == 0)
            Destroy(obj);
    }
}