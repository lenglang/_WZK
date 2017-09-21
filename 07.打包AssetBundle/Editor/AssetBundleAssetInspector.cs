using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
namespace WZK
{
    public class AssetBundleAssetInspector
    {
        private List<string> assetPaths = new List<string>();
        public AssetBundleAssetInspector(Object[] targets)
        {
            var paths = new List<string>();
            for (int i = 0; i < targets.Length; i++)
            {
                string assetPath = AssetDatabase.GetAssetPath(targets[i]);
                if (File.Exists(assetPath) && assetPath.EndsWith(".unity3d"))
                    paths.Add(assetPath);
            }

            if (paths.Count == targets.Length)
            {
                foreach (var path in paths)
                {
                    var assetBundle = AssetBundle.LoadFromFile(path);
                    if (assetBundle != null)
                    {
                        assetPaths.AddRange(assetBundle.GetAllAssetNames());
                        assetPaths.AddRange(assetBundle.GetAllScenePaths());
                        assetBundle.Unload(true);
                    }
                }
            }
        }
        ~AssetBundleAssetInspector()
        {

        }

        public void OnInspectorGUI()
        {
            for (int i = 0; i < assetPaths.Count; i++)
            {
                var assetPath = assetPaths[i];
                if (GUILayout.Button(new GUIContent(assetPath, AssetDatabase.GetCachedIcon(assetPath)), "Label"))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                    Selection.activeObject = asset;
                }
            }
        }
    }
}