using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace WZK
{
    /// <summary>
    /// 位置信息编辑器扩展
    /// </summary>
    [CustomEditor(typeof(GameObjectPosition))]
    public class GameObjectPositionEditor : Editor
    {
        private GameObjectPosition _gameObjectPosition;
        private TransformInformation _information;
        private TransformInformation _currentInformation;
        private string _flodOutName;
        private int _deletePositionIndex = -1;
        private bool _creating = false;
        public static PositionScriptableObject PSO;
        private string _psoPath = "Assets/动态保存位置.asset";
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _gameObjectPosition = target as GameObjectPosition;
            GUILayout.Space(30);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("添加位置"))
            {
                _gameObjectPosition._list.Add(new TransformInformation());
                SavePosition(_gameObjectPosition._list[_gameObjectPosition._list.Count - 1], _gameObjectPosition.gameObject.GetComponent<Transform>());
            }
            EditorGUILayout.EndHorizontal();
            _deletePositionIndex = -1;
            for (int j = 0; j < _gameObjectPosition._list.Count; j++)
            {
                _information = _gameObjectPosition._list[j];
                EditorGUILayout.BeginHorizontal();
                _information._desc = EditorGUILayout.TextField(_information._desc);
                if (GUILayout.Button("切到本地坐标"))
                {
                    Transform transform = _gameObjectPosition.transform;
                    transform.localPosition = _information._localPosition;
                    transform.rotation = _information._rotation;
                    transform.localScale = _information._localScale;
                }
                if (GUILayout.Button("切到世界坐标"))
                {
                    Transform transform = _gameObjectPosition.transform;
                    transform.position = _information._position;
                    transform.rotation = _information._rotation;
                    transform.localScale = _information._localScale;
                }
                if (GUILayout.Button("保存位置"))
                {
                    SavePosition(_information, _gameObjectPosition.transform);
                    if (EditorApplication.isPlaying)
                    {
                        _currentInformation = _information;
                        EditorApplication.delayCall += CreateScriptableObject;
                    }
                    Debug.Log("保存成功");
                }
                if (GUILayout.Button("更新运行状态保存的位置")&& EditorApplication.isPlaying==false)
                {
                    PSO = AssetDatabase.LoadAssetAtPath<PositionScriptableObject>(_psoPath);
                    if (PSO != null)
                    {
                        TransformInformation t = PSO._positionList.Find(n => n._gameObject == _gameObjectPosition.gameObject && n._desc == _information._desc);
                        if (t != null)
                        {
                            _gameObjectPosition._list[j] = t;
                            Debug.Log("更新成功");
                        }
                    }
                }
                if (GUILayout.Button("删除"))
                {
                    if (EditorUtility.DisplayDialog("警告", "你确定要删除当前保存的位置信息吗？", "确定", "取消"))
                    {
                        _deletePositionIndex = j;
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20);
            }
            if (_deletePositionIndex != -1) _gameObjectPosition._list.RemoveAt(_deletePositionIndex);
            GUILayout.Space(1000);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(_gameObjectPosition);
            if (GUI.changed&& EditorApplication.isPlaying==false) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        /// <summary>
        /// 保存位置
        /// </summary>
        /// <param name="information"></param>
        /// <param name="transform"></param>
        private void SavePosition(TransformInformation information, Transform transform)
        {
            information._localPosition = transform.localPosition;
            information._position = transform.position;
            information._rotation = transform.rotation;
            information._localScale = transform.localScale;
            information._angle = transform.localEulerAngles;
            if (EditorApplication.isPlaying) information._gameObject = transform.gameObject;
        }
        /// <summary>
        /// 创建序列化脚本对象
        /// </summary>
        private void CreateScriptableObject()
        {
            if (PSO == null)
            {
                PSO = AssetDatabase.LoadAssetAtPath<PositionScriptableObject>(_psoPath);
                if (PSO==null)
                {
                    PSO = ScriptableObject.CreateInstance<PositionScriptableObject>();
                    AssetDatabase.CreateAsset(PSO, _psoPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
                PSO._positionList.Clear();
            }
            TransformInformation t = PSO._positionList.Find(n => n._gameObject == _currentInformation._gameObject && n._desc == _currentInformation._desc);
            if (t != null) PSO._positionList.Remove(t);
            PSO._positionList.Add(_currentInformation);
        }
    }
}
