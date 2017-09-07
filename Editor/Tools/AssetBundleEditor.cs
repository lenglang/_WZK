using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
namespace WZK
{
    /// <summary>
    /// AssetBundle编辑器打包扩展
    /// </summary>
    public class AssetBundleEditor : Editor
    {
        [MenuItem("AssetBundle/分别打包选中文件为AssetBundle-压缩")]
        static void BulidCompressedAssetBundle()
        {
            BuildAssetBundle();
        }
        [MenuItem("AssetBundle/分别打包选中文件为AssetBundle-不压缩")]
        static void BulidUncompressedAssetBundle()
        {
            BuildAssetBundle(false);
        }
        static void BuildAssetBundle(bool IscompressedAssetBundle=true)
        {
            if (Selection.objects.Length == 0)
                return;
            Object[] objects = Selection.objects;
            for (int i = 0; i < objects.Length; i++)
            {
                BuildOne(objects[i], IscompressedAssetBundle);
            }
        }
        static void BuildOne(Object obj, bool IscompressedAssetBundle=true)
        {
            AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetNames = new string[1];
            assetBundleBuild.assetNames[0] = AssetDatabase.GetAssetPath(obj);
            string directoryName = Path.GetDirectoryName(assetBundleBuild.assetNames[0]);
            assetBundleBuild.assetBundleName = Path.GetFileName(assetBundleBuild.assetNames[0]) + ".unity3d";
#if UNITY_ANDROID
            directoryName += "/Android";
#endif
#if UNITY_IOS
        directoryName += "/iOS";
#endif
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            Debug.Log(directoryName);
#if UNITY_ANDROID
            if(IscompressedAssetBundle){BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.None, BuildTarget.Android);}
            else{BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);}
#endif

#if UNITY_IOS
            if (IscompressedAssetBundle) { BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.None, BuildTarget.iOS); }
            else { BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.iOS); }
#endif
        }

        [MenuItem("AssetBundle/打包选中文件为一个AssetBundle-压缩")]
        static void BulidCompressedAssetBundleAll()
        {
            BuildAll();
        }
        [MenuItem("AssetBundle/打包选中文件为一个AssetBundle-不压缩")]
        static void BulidUncompressedAssetBundleAll()
        {
            BuildAll(false);
        }
        static void BuildAll(bool IscompressedAssetBundle = true)
        {
            if (Selection.objects.Length == 0)
                return;
            AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetNames = new string[Selection.objects.Length];
            for (int i = 0; i < Selection.objects.Length; i++)
                assetBundleBuild.assetNames[i] = AssetDatabase.GetAssetPath(Selection.objects[i]);
            string directoryName = Path.GetDirectoryName(assetBundleBuild.assetNames[0]);
            if (Selection.objects.Length == 1)
                assetBundleBuild.assetBundleName = Path.GetFileName(assetBundleBuild.assetNames[0]) + ".unity3d";
            else
                assetBundleBuild.assetBundleName = Path.GetFileName(directoryName) + ".unity3d";
#if UNITY_ANDROID
        directoryName += "/Android";
#endif
#if UNITY_IOS
            directoryName += "/iOS";
#endif
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

#if UNITY_ANDROID
        if(IscompressedAssetBundle){BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.None, BuildTarget.Android);}
         else{BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);}
#endif

#if UNITY_IOS
            if (IscompressedAssetBundle) { BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.None, BuildTarget.iOS); }
            else { BuildPipelineHelper.BuildAssetBundles(directoryName, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.iOS); }
#endif
        }
    }
}
