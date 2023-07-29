namespace SAL.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class EnumGridDataWindow : EditorWindow
    {
        private EnumGridData enumGridData;
        
        [MenuItem("Window/Enum Grid Data")]
        private static void Init()
        {
            EnumGridDataWindow window = (EnumGridDataWindow)EditorWindow.GetWindow(typeof(EnumGridDataWindow));
            window.titleContent = new GUIContent("Enum Grid Data");
            window.Show();
        }

        private void OnEnable()
        {
            enumGridData =
                AssetDatabase.LoadAssetAtPath<EnumGridData>(
                    "Assets/EnumGridData.asset"); // Replace with the path to your EnumGridData asset
        }

        private void OnGUI()
        {
            if (enumGridData == null)
            {
                EditorGUILayout.HelpBox("Enum Grid Data asset not found. Create or assign an Enum Grid Data asset.",
                    MessageType.Error);
                return;
            }

            EditorGUILayout.LabelField("Enum Grid:");

            EditorGUI.BeginChangeCheck();

            for (int y = 0; y < 10; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < 10; x++)
                {
                    enumGridData.grid[x, y] = (EnumGridData.MyEnum)EditorGUILayout.EnumPopup(enumGridData.grid[x, y]);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(enumGridData);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Save to Text File"))
            {
                SaveToTextFile();
            }
        }

        private void SaveToTextFile()
        {
            string path = EditorUtility.SaveFilePanel("Save Enum Grid Data", "", "enumGridData.txt", "txt");

            if (string.IsNullOrEmpty(path))
                return;

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path))
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        writer.Write(enumGridData.grid[x, y].ToString() + "\t");
                    }

                    writer.WriteLine();
                }
            }

            Debug.Log("Enum Grid Data saved to: " + path);
        }
    }

}