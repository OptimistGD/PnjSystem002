using System.Linq;
using PNJSystem.Core;
using UnityEditor;
using UnityEngine;

namespace PNJSystem.Gameplay
{
    [ExecuteAlways]
    public class PnjEditor : MonoBehaviour
    {
        private PnjAdaptator pnjAdaptator;
        
        private void OnEnable()
        {
            pnjAdaptator = GetComponent<PnjAdaptator>();
        }

        //OnGUI : appelé À CHAQUE FRAME par Unity => editor
        private void OnGUI()
        {
            var profession = pnjAdaptator.Editor_ProfessionData;
            
            // Sécurité
            if (pnjAdaptator == null) 
                return;
            // Sécurité
            if (profession == null) 
                return;

            
            GUILayout.Label($"Métier : {profession.description}");
            GUILayout.Label("Inventaire :");

            // Cas 1 : inventaire vide
            if (profession.startingItems == null || 
                profession.startingItems.Count == 0)
            {
                GUILayout.Label("(vide)");
            }
            else
            {
                // Cas 2 : on parcourt et affiche chaque item
                foreach (var item in profession.startingItems)
                {
                    // Sécurité
                    if (item == null) continue;
                    GUILayout.Label("• " + item.description);
                }
            }
        }
    }
}