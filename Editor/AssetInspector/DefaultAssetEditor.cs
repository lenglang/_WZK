using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(DefaultAsset))]
public class DefaultAssetEditor : Editor
{
    private FolderAssetInspector folderAssetInspector;
    private AssetBundleAssetInspector assetBundleAssetInspector;


    void Awake()
    {
        folderAssetInspector = new FolderAssetInspector(targets);
        assetBundleAssetInspector = new AssetBundleAssetInspector(targets);
    }

    void OnDestroy()
    {

    }

    public override void OnInspectorGUI()
    {
        GUI.enabled = true;

        if(folderAssetInspector != null)
            folderAssetInspector.OnInspectorGUI();

        if(assetBundleAssetInspector != null)
            assetBundleAssetInspector.OnInspectorGUI();
    }
}