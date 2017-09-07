using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace WZK
{
    [CustomEditor(typeof(ResourcesScriptableObject))]
    public class ResourcesScriptableObjectEditor : Editor
    {
        private ResourcesScriptableObject _resourcesScriptableObject;
        private string _directionPath;//文件夹路径
        private string _fileAssetPath;//文件工程目录
        private bool _isExist;//是否已存在
        private bool _isDelete;//删除资源
        public List<string> _extensionList = new List<string> { ".mp3", ".ogg", ".asset", ".txt", ".xml", ".mat", ".prefab", ".png", ".jpg" };//选择的扩展名列表
        public override void OnInspectorGUI()
        {
            _resourcesScriptableObject = target as ResourcesScriptableObject;
            EditorGUILayout.LabelField("只允许拖文件夹！！！！！！！！！！！");
            int index = -1;
            for (int i = 0; i < _extensionList.Count; i++)
            {
                if (i % 4 == 0) EditorGUILayout.BeginHorizontal();
                index = _resourcesScriptableObject._choseExtensionList.IndexOf(_extensionList[i]);
                if (GUILayout.Button(_extensionList[i]))
                {
                    if (index == -1)
                    {
                        _resourcesScriptableObject._choseExtensionList.Add(_extensionList[i]);
                    }
                    else
                    {
                        _resourcesScriptableObject._choseExtensionList.RemoveAt(index);
                    }
                }
                if((i>1&&i%4==3)||i==_extensionList.Count-1) EditorGUILayout.EndHorizontal();
            }
            GUILayout.Space(30);
            if (_resourcesScriptableObject._choseExtensionList.Count == 0)
            {
                EditorGUILayout.LabelField("没有选择指定的后缀，默认包含以上所有后缀！");
            }
            else
            {
                string str = "";
                for (int i = 0; i < _resourcesScriptableObject._choseExtensionList.Count; i++)
                {
                    str += _resourcesScriptableObject._choseExtensionList[i];
                    if (i < _resourcesScriptableObject._choseExtensionList.Count - 1)
                    {
                        str += "、";
                    }
                }
                EditorGUILayout.LabelField("选择的后缀:"+str);
            }
            List<ResourcesScriptableObject.Config> objList = _resourcesScriptableObject._objectList;
            for (int i = 0; i < objList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                objList[i]._object = (Object)EditorGUILayout.ObjectField("对象" + (i + 1), objList[i]._object, typeof(Object), false);
                _isDelete = false;
                if (GUILayout.Button("删除" + (i + 1)))
                {
                    _isDelete = true;
                }
                EditorGUILayout.EndHorizontal();
                objList[i]._assetPath = EditorGUILayout.TextField("路径" + (i + 1), objList[i]._assetPath);
                GUILayout.Space(10);
                if (objList[i]._assetPath == "" && objList[i]._object) objList[i]._assetPath = objList[i]._object.name;
                if (_isDelete) objList.RemoveAt(i);
            }
            if (Event.current.type == EventType.DragExited)
            {
                Debug.Log(DragAndDrop.objectReferences[0].GetType());
                if (DragAndDrop.objectReferences[0].GetType() == typeof(Texture2D) ||DragAndDrop.objectReferences[0].GetType() == typeof(AudioClip)|| DragAndDrop.objectReferences[0].GetType() == typeof(GameObject))
                {
                    AddObject(objList,DragAndDrop.objectReferences[0], DragAndDrop.paths[0]);
                }
                else
                {
                    _directionPath = Application.dataPath;
                    _directionPath = _directionPath.Substring(0, _directionPath.LastIndexOf("/") + 1) + DragAndDrop.paths[0];
                    if (Directory.Exists(_directionPath))
                    {
                        DirectoryInfo direction = new DirectoryInfo(_directionPath);
                        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                        for (int i = 0; i < files.Length; i++)
                        {
                            if (_resourcesScriptableObject._choseExtensionList.Count == 0 && _extensionList.Contains(Path.GetExtension(files[i].FullName)) == false)
                            {
                                continue;
                            }
                            else if (_resourcesScriptableObject._choseExtensionList.Count > 0 && _resourcesScriptableObject._choseExtensionList.Contains(Path.GetExtension(files[i].FullName)) == false)
                            {
                                continue;
                            }
                            _fileAssetPath = files[i].DirectoryName;
                            _fileAssetPath = _fileAssetPath.Substring(_fileAssetPath.IndexOf("Assets")) + "/" + files[i].Name;
                            AddObject(objList, AssetDatabase.LoadAssetAtPath<Object>(_fileAssetPath), _fileAssetPath);
                        }
                    }
                }
            }
            GUILayout.Space(30);
            if (GUILayout.Button("清空")&&EditorUtility.DisplayDialog("警告", "确定要清空所有数据吗", "确定", "取消"))
            {
                _resourcesScriptableObject._objectList.Clear();
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(_resourcesScriptableObject);
        }
        /// <summary>
        /// 添加音频
        /// </summary>
        private void AddObject(List<ResourcesScriptableObject.Config> objList, Object obj, string assetPath)
        {
            Debug.Log(obj.GetType());
            _isExist = false;
            assetPath = assetPath.Substring(0, assetPath.IndexOf("."));
            assetPath = assetPath.Replace("\\", "/");
            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i]._object == obj)
                {
                    _isExist = true;
                    objList[i]._assetPath = assetPath;
                    Debug.LogError("配置表里已存在该对象");
                    break;
                }
            }
            if (_isExist == false) objList.Add(new ResourcesScriptableObject.Config(obj, assetPath));
        }

        [MenuItem("GameObject/自定义/创建合包资源管理对象", false, MenuItemConfig.合包资源管理)]
        private static void CreateSoundManagerObject()
        {
            GameObject gameObject = new GameObject("合包资源管理");
            gameObject.AddComponent<MyResources>();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = gameObject;
            EditorGUIUtility.PingObject(Selection.activeObject);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create GameObject");
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            //GameObject obj = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Common/Prefabs/Sound/声音管理.prefab")).name = "声音管理";
        }

    }
}
