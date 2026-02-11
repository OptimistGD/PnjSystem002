using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PNJSystem.Core
{
    public class PnjUI : MonoBehaviour
    {
        [SerializeField] private PnjAdaptator pnjAdaptator;
        [SerializeField] private Text output;
        
        public void ShowItems()
        {
            var items = pnjAdaptator.GetItems().ToList();

            Debug.Log($"[UI] Items reçus : {items.Count}");

            output.text = items.Count == 0 ? "Aucun objet" : string.Join("\n", items);
        }
        
        public void TakeItem()
        {
            PnjAdaptator.GiveFirstItemToPlayer();
            ShowItems();
        }
    }
}