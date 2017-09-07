using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SceneAsset))]
public class SceneAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUI.enabled = true;

        base.OnInspectorGUI();

        if (GUILayout.Button("选择依赖"))
        {
            EditorApplication.ExecuteMenuItem("Assets/Select Dependencies");
        }
    }
}