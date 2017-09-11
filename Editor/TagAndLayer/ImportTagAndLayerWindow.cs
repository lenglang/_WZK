using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace WZK
{
    public class ImportTagAndLayerWindow : EditorWindow
    {
        private string _tagText = "";
        private string _layerText = "";
        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            _tagText=EditorGUILayout.TextField("Tag", _tagText);
            Add("tags", 7, _tagText);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _layerText=EditorGUILayout.TextField("Layer", _layerText);
            Add("layers", 5, _layerText);
            EditorGUILayout.EndHorizontal();
        }
        private void Add(string type,int limit,string addText)
        {
            if (GUILayout.Button("导入"))
            {
                List<string> list = new List<string>();
                if (type == "tags")
                { list.AddRange(UnityEditorInternal.InternalEditorUtility.tags); }
                else
                {
                    list.AddRange(UnityEditorInternal.InternalEditorUtility.layers);
                }
                list.RemoveRange(0, limit);
                string[] addList = addText.Split(',');
                for (int i = 0; i < addList.Length; i++)
                {
                    if (list.IndexOf(addList[i]) == -1) { list.Add(addList[i]); }
                    else { Debug.LogError("添加的" + type + "集，存在相同字符,被自动过滤掉"); }
                }
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty it = tagManager.GetIterator();
                while (it.NextVisible(true))
                {
                    if (it.name == "tags"&&it.name==type)
                    {
                        it.ClearArray();
                        it.arraySize = list.Count;
                        for (int i = 0; i < it.arraySize; i++)
                        {
                            SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                            dataPoint.stringValue = list[i];
                        }
                        tagManager.ApplyModifiedProperties();
                    }
                    if (it.name == "layers" && it.name == type)
                    {
                        it.ClearArray();
                        int start = 8;
                        it.arraySize = start + list.Count;
                        for (int i = start; i < it.arraySize; i++)
                        {
                            SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                            dataPoint.stringValue = list[i - start];
                        }
                        tagManager.ApplyModifiedProperties();
                    }
                }
            }
            if (GUILayout.Button("清空") && EditorUtility.DisplayDialog("警告", "你确定要清空所有"+type+"吗？", "确定", "取消"))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty it = tagManager.GetIterator();
                while (it.NextVisible(true))
                {
                    if (it.name == "tags" && it.name == type)
                    {
                        it.ClearArray();
                        tagManager.ApplyModifiedProperties();
                    }
                    if (it.name == "layers" && it.name == type)
                    {
                        it.ClearArray();
                        tagManager.ApplyModifiedProperties();
                    }
                }
            }
        }
    }
}
