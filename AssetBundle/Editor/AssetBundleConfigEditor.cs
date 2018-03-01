using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
namespace WZK { 
    /// <summary>
    /// 打包编辑器
    /// </summary>
    [CustomEditor(typeof(AssetBundleConfig))]
    public class AssetBundleConfigEditor : Editor
    {
        private AssetBundleConfig abc;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            abc = target as AssetBundleConfig;
            if (string.IsNullOrEmpty(abc._savePath))
            {
                abc._savePath = Application.streamingAssetsPath;
                Debug.Log("persistentDataPath:" + Application.persistentDataPath);
                Debug.Log("streamingAssetsPath:" + Application.streamingAssetsPath);
            }
            abc._savePath = EditorGUILayout.TextField("统一保存路径", abc._savePath);
            AssetBundleConfig.Config config;
            GUILayout.Space(20);
            int delteIndex = -1;
            for (int i = 0; i < abc._SelectionObjects.Count; i++)
            {
                config = abc._SelectionObjects[i];
                config._bool = EditorGUILayout.Toggle("是否打包",config._bool);
                config._savePath = EditorGUILayout.TextField("保存路径", config._savePath);
                config._saveName = EditorGUILayout.TextField("保存名字", config._saveName);
                config._object = EditorGUILayout.ObjectField("打包对象",config._object, typeof(Object), false);
                if (GUILayout.Button("删除") && EditorUtility.DisplayDialog("警告", "确定要删除该数据吗", "确定", "取消"))
                {
                    delteIndex = i;
                }
                GUILayout.Space(50);
            }
            if (delteIndex != -1) abc._SelectionObjects.RemoveAt(delteIndex);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("新建"))
            {
                abc.CreateObject(abc._savePath);
            }
            if (GUILayout.Button("一键打包")&&abc._SelectionObjects.Count>0)
            {
                EditorApplication.delayCall += CreateNewScene;//延迟调用，防报错
            }
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(abc);
            if (GUI.changed)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        private void CreateNewScene()
        {
            AssetBundleConfig.Config cg;
            for (int i = 0; i < abc._SelectionObjects.Count; i++)
            {
                cg = abc._SelectionObjects[i];
                if (cg._bool) AssetBundleEditor.BuildOne(cg._object, true, cg._savePath, cg._saveName);
            }
        }
    }
}
