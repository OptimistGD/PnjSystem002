using UnityEngine;

namespace FactorySystem.Samples.Merchants
{
    public class TestConveyorBelt : ConveyorBelt<Fruit>
    {
        [SerializeField]
        private Transform _endPoint;
        
        protected override Vector3 GetEndPoint()
        {
            return _endPoint.position;
        }
        
        protected override void OnItemArrived(GameObject obj)
        {
            Debug.Log($"[TestConveyorBelt] Item arrivé à destination : {obj.name}");
            
            base.OnItemArrived(obj);
        }
    }
}