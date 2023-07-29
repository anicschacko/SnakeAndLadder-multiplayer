
using System;
using UnityEditor.Build.Content;
using UnityEngine;

namespace SAL.Editor
{
    using SAL.Data;
    using UnityEditor;

    [CustomEditor(typeof(GameConfig))]
    public class GameConfigEditor : Editor
    {
        private SerializedProperty listData;
        private GameConfig gameConfigScript;
        
        private void OnEnable()
        {
            listData = serializedObject.FindProperty("TilesData");
            gameConfigScript = (GameConfig)target;
        }

        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "TilesData");
            
            if (GUILayout.Button("LoadData"))
            {
                gameConfigScript.SetData();
                serializedObject.Update();
            }

            EditorGUILayout.PropertyField(listData);
            serializedObject.ApplyModifiedProperties();
        }
    }

}