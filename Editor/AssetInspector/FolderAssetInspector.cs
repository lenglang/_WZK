using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class FolderAssetInspector
{
    private Object[] targets;

    private string filter;
    private string[] searchInFolders;

    private string[] assets;

    public FolderAssetInspector(Object[] targets)
    {
        this.targets = targets;

        var folders = new List<string>();

        for (int i = 0; i < targets.Length; i++)
        {
            string assetPath = AssetDatabase.GetAssetPath(targets[i]);
            if(Directory.Exists(assetPath))
                folders.Add(assetPath);
        }

        searchInFolders = folders.ToArray();
    }

    void OnDestroy()
    {

    }

    public void OnInspectorGUI()
    {
        if(searchInFolders.Length != targets.Length)
            return;

        filter = EditorGUILayout.TextField(filter);

        if(GUILayout.Button("查找"))
        {
            assets = AssetDatabase.FindAssets(filter, searchInFolders);
        }

        for (int i = 0; assets != null && i < assets.Length; i++)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assets[i]);
            if(GUILayout.Button(new GUIContent(assetPath, AssetDatabase.GetCachedIcon(assetPath)), "Label"))
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                Selection.activeObject = asset;
            }
        }
    }
}