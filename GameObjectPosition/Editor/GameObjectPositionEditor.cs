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
        private string _flodOutName;
        private int _deletePositionIndex = -1;
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
                if (GUILayout.Button("切换到该位置(本地)"))
                {
                    Transform transform = _gameObjectPosition.gameObject.GetComponent<Transform>();
                    transform.localPosition = _information._localPosition;
                    transform.rotation = _information._rotation;
                    transform.localScale = _information._localScale;
                }
                if (GUILayout.Button("切换到该位置(世界)"))
                {
                    Transform transform = _gameObjectPosition.gameObject.GetComponent<Transform>();
                    transform.position = _information._position;
                    transform.rotation = _information._rotation;
                    transform.localScale = _information._localScale;
                }
                if (GUILayout.Button("保存当前位置"))
                {
                    SavePosition(_information, _gameObjectPosition.gameObject.GetComponent<Transform>());
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
        }
    }
}
