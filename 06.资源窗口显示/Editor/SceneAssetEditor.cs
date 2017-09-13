using UnityEngine;
using UnityEditor;
namespace WZK
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SceneAsset))]
    public class SceneAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.enabled = true;

            base.OnInspectorGUI();

            if (GUILayout.Button("查看依赖"))
            {
                EditorApplication.ExecuteMenuItem("Assets/Select Dependencies");
            }
        }
    }
}