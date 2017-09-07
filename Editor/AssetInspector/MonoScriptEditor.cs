using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(MonoScript))]

public class MonoScriptEditor : Editor
{
    private string assetPath;

    private string originalText;
    private string modifiedText;

    void Awake()
    {
        GUI.enabled = true;

        assetPath = AssetDatabase.GetAssetPath(target);

        originalText = File.ReadAllText(assetPath);
        modifiedText = originalText.Clone() as string;
    }

    void OnDestroy()
    {
        if (modifiedText != originalText)
        {
            if (EditorUtility.DisplayDialog("", "是否保存 " + assetPath + " ？", "确定", "取消"))
            {
                File.WriteAllText(assetPath, modifiedText);
                AssetDatabase.Refresh();
            }
        }
    }

    public override void OnInspectorGUI()
    {
        modifiedText = EditorGUILayout.TextArea(modifiedText);
    }
}