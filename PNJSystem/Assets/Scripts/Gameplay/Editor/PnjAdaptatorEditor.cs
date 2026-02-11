using System.Linq;
using PNJSystem.Core;
using UnityEditor;
using UnityEngine;

namespace PNJSystem.Gameplay

{
    [CustomEditor(typeof(PnjAdaptator))]
    public class NpcViewEditor : Editor
    {
        private bool showInventory;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var npcView = (PnjAdaptator)target;
            var profession = npcView.Editor_ProfessionData;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("=== DEBUG (EDITOR) ===",
                EditorStyles.boldLabel);

            if (profession == null)
            {
                EditorGUILayout.HelpBox(
                    "Aucune ProfessionData assignée",
                    MessageType.Warning);
                return;
            }

            EditorGUILayout.LabelField("Profession", profession.description);

            showInventory = EditorGUILayout.Foldout(showInventory,"Inventaire de départ");

            if (showInventory)
            {
                if (profession.startingItems == null ||
                    profession.startingItems.Count == 0)
                {
                    EditorGUILayout.LabelField("(Aucun objet)");
                }
                else
                {
                    foreach (var item in profession.startingItems)
                    {
                        if (item == null) continue;

                        EditorGUILayout.LabelField(
                            $"• {item.description} ({item.id})");
                    }
                }
            }
        }
    }
}

