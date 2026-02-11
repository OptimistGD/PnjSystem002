using System;
using System.Collections.Generic;
using System.Linq;
using PNJSystem.Core.Inventory;
using PNJSystem.Core.Items;
using PNJSystem.Core.Utilities;
using UnityEngine;

namespace PNJSystem.Core
{
    //communique avec unity (pont entre la logic et editor/ ui)
    public class PnjAdaptator : MonoBehaviour
    {
        [SerializeField] 
        private ProfessionData professionData;
        private static PnjController pnj;
        
        // Le code qui reçoit cette liste IEnumerable ne peut QUE la lire 
        //Ici, prend le liste et l'assigne au PNJ
        public IEnumerable<string> GetItems()
        {
            return pnj.GetItems().Select(i => i.Id);
        }
        
        //Editor => passe les informations Items + Profession
        public ProfessionData Editor_ProfessionData => professionData;

        public IEnumerable<ItemData> Editor_StartingItems =>
            professionData != null
                ? professionData.startingItems
                : Enumerable.Empty<ItemData>();

        //utile
        private void Awake()
        {
            InitializeNpc();
        }
        private void InitializeNpc()
        {
            if (professionData == null)
            {
                //Debug.LogWarning("[NpcView] ProfessionData manquante", this);
                return;
            }

            pnj = PnjFactory.Create(professionData);
            //Debug.Log($"[PnjAdaptator] Init NPC ({(Application.isPlaying ? "Play" : "Editor")})",this);
        }
        
        //méthode pour passer un item (si ya)
        public static void GiveFirstItemToPlayer()
        {
            var item = pnj.GetItems().FirstOrDefault();
            
            if (item == null)
            {
                //Debug.Log("[PnjAdaptator] Aucun objet à donner");
                return;
            }
            pnj.GiveItem(item.Id);
            //Debug.Log($"[PnjAdaptator] Objet donné : {item.Name}");
        }
        
//ne se lit que si il y a un mode editor
#if UNITY_EDITOR
        private void OnValidate()
        {
            // Appelé dans l’Editor quand une valeur change
            if (!Application.isPlaying)
            {
                InitializeNpc();
            }
        }
#endif
        
    }
}