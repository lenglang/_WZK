using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
namespace WZK
{
    /// <summary>
    /// 声音配置编辑器扩展
    /// </summary>
    [CustomEditor(typeof(SoundConfig))]
    public class SoundConfigEditor : Editor
    {
        private string[] _typeButtons = { "人声", "音效"};//类型按钮切换
        private bool _isDelete;//是否删除
        private bool _isExist;//是否存在
        private string _directionPath;//文件夹路径
        private string _fileAssetPath;//文件工程目录
        private int _selectType = 0;//选择的类型
        private bool _isOtherLanguage = false;//是否其他语言
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SoundConfig soundConfig = target as SoundConfig;
            soundConfig._savePath = EditorGUILayout.TextField("生成的枚举类存放的Assets下文件夹路径", soundConfig._savePath);
            if (GUILayout.Button("生成枚举配置"))
            {
                if (string.IsNullOrEmpty(soundConfig._fileName) || string.IsNullOrEmpty(soundConfig._nameSpace))
                {
                    EditorUtility.DisplayDialog("警告", "脚本名或命名空间未填写", "知道了");
                    return;
                }
                string path = AssetDatabase.GetAssetOrScenePath(Selection.activeObject);
                if (string.IsNullOrEmpty(soundConfig._savePath))
                {
                    path = Application.dataPath + path.Substring(path.IndexOf("/"));
                    path = Path.GetDirectoryName(path);
                }
                else
                {
                    path = Application.dataPath + "/" + soundConfig._savePath;
                }
                path += "/" + soundConfig._fileName + ".cs";
                if (File.Exists(path) && EditorUtility.DisplayDialog("警告", "已存在该配置，是否覆盖：" + path, "是的", "取消")) //显示对话框  
                {
                    CreateCSConfig(soundConfig, path);
                }
                else
                {
                    CreateCSConfig(soundConfig, path);
                }
            }
            soundConfig._fileName = EditorGUILayout.TextField("脚本名", soundConfig._fileName);
            soundConfig._nameSpace = EditorGUILayout.TextField("命名空间", soundConfig._nameSpace);
            _selectType = GUILayout.Toolbar(_selectType, _typeButtons);
            List<SoundConfig.Config> scList;
            _isOtherLanguage = false;
            switch (_selectType)
            {
                case 0:
                    EditorGUILayout.LabelField("枚举名", soundConfig._voiceEnumType);
                    scList = soundConfig._voiceList;
                    break;
                case 1:
                    EditorGUILayout.LabelField("枚举名", soundConfig._soundEnumType);
                    scList = soundConfig._soundList;
                    break;
                default:
                    scList = soundConfig._soundList;
                    break;
            }
            ChineseLanguage(scList);
            GUILayout.Space(1000);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(soundConfig);
            if (GUI.changed)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        /// <summary>
        /// 创建脚本枚举配置
        /// </summary>
        /// <param name="soundConfig"></param>
        /// <param name="path"></param>
        private void CreateCSConfig(SoundConfig soundConfig, string path)
        {
            string audioConfig = "";
            CreateEnum(soundConfig._voiceList, ref audioConfig, soundConfig._voiceEnumType);
            audioConfig += "\n";
            CreateEnum(soundConfig._soundList, ref audioConfig, soundConfig._soundEnumType);
            string config = "using System.ComponentModel;" + "\n"
                + "namespace " + soundConfig._nameSpace + "\n"
                + "{" + "\n"
                + audioConfig
                + "\n"
                + "}";
            File.WriteAllText(path, config);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 枚举创建
        /// </summary>
        /// <param name="scList"></param>
        /// <param name="audioConfig"></param>
        /// <param name="enumType"></param>
        private void CreateEnum(List<SoundConfig.Config> scList, ref string audioConfig, string enumType)
        {
            audioConfig += "public enum " + enumType + "\n"
                + "{" + "\n";
            for (int i = 0; i < scList.Count; i++)
            {
                audioConfig += "[Description(" + '"' + scList[i]._audioClip.name + '"' + ")]" + "\n";
                audioConfig += scList[i]._desc;
                if (i != scList.Count - 1)
                {
                    audioConfig += "," + "\n";
                }
            }
            audioConfig += "\n}";
        }
        /// <summary>
        /// 中文语言
        /// </summary>
        private void ChineseLanguage(List<SoundConfig.Config> scList)
        {
            for (int i = 0; i < scList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                scList[i]._audioClip = (AudioClip)EditorGUILayout.ObjectField("声音" + (i + 1), scList[i]._audioClip, typeof(AudioClip), false);
                _isDelete = false;
                if (GUILayout.Button("删除" + (i + 1)))
                {
                    _isDelete = true;
                }
                EditorGUILayout.EndHorizontal();
                scList[i]._desc = EditorGUILayout.TextField("描述" + (i + 1), scList[i]._desc);
                GUILayout.Space(10);
                if (scList[i]._desc == "" && scList[i]._audioClip) scList[i]._desc = scList[i]._audioClip.name;
                if (_isDelete) scList.RemoveAt(i);
            }
            if (Event.current.type == EventType.DragExited)
            {
                if (DragAndDrop.objectReferences[0].GetType() == typeof(AudioClip))
                {
                    AddAudioClip(scList, (AudioClip)DragAndDrop.objectReferences[0], DragAndDrop.paths[0]);
                }
                else
                {
                    if (DragAndDrop.objectReferences[0].GetType().ToString() == "UnityEditor.DefaultAsset")
                    {
                        _directionPath = Application.dataPath;
                        _directionPath = _directionPath.Substring(0, _directionPath.LastIndexOf("/") + 1) + DragAndDrop.paths[0];
                        if (Directory.Exists(_directionPath))
                        {
                            DirectoryInfo direction = new DirectoryInfo(_directionPath);
                            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                            for (int i = 0; i < files.Length; i++)
                            {
                                _fileAssetPath = files[i].DirectoryName;
                                _fileAssetPath = _fileAssetPath.Substring(_fileAssetPath.IndexOf("Assets")) + "/" + files[i].Name;
                                if (AssetDatabase.LoadAssetAtPath<AudioClip>(_fileAssetPath)) AddAudioClip(scList, AssetDatabase.LoadAssetAtPath<AudioClip>(_fileAssetPath), _fileAssetPath);
                            }
                        }
                    }
                }
            }
            GUILayout.Space(30);
            if (GUILayout.Button("清空") && scList.Count != 0)
            {
                if (EditorUtility.DisplayDialog("警告", "你确定要清空列表里的数据吗？", "确定", "取消"))
                {
                    scList.Clear();
                }
            }
        }
        /// <summary>
        /// 添加音频
        /// </summary>
        private void AddAudioClip(List<SoundConfig.Config> scList, AudioClip ac, string resourcesPath)
        {
            resourcesPath = resourcesPath.Replace("\\", "/");
            if (resourcesPath.Contains("Resources")) resourcesPath = resourcesPath.Substring(resourcesPath.IndexOf("Resources/") + 10);
            resourcesPath = Path.GetDirectoryName(resourcesPath) + "/" + Path.GetFileNameWithoutExtension(resourcesPath);
            _isExist = false;
            for (int i = 0; i < scList.Count; i++)
            {
                if (scList[i]._audioClip == ac)
                {
                    _isExist = true;
                    Debug.LogError("配置表里已存在该音频");
                    break;
                }
            }
            if (_isExist == false) scList.Add(new SoundConfig.Config(ac, resourcesPath));
        }
        [MenuItem("GameObject/WZK/创建声音管理对象", false, 16)]
        private static void CreateSoundManagerObject()
        {
            GameObject gameObject = new GameObject("声音管理");
            gameObject.AddComponent<SoundManager>();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = gameObject;
            EditorGUIUtility.PingObject(Selection.activeObject);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create GameObject");
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            //GameObject obj = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Common/Prefabs/Sound/声音管理.prefab")).name = "声音管理";
        }
    }
}
