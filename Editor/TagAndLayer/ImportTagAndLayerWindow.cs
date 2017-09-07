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
            if (GUILayout.Button("导入"))
            {
                List<string> tags = new List<string>();
                tags.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                tags.RemoveRange(0, 7);
                string[] newTags = _tagText.Split(',');


                for (int i = 0; i < newTags.Length; i++)
                {
                    if (tags.IndexOf(newTags[i]) == -1) tags.Add(newTags[i]);
                }

                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty it = tagManager.GetIterator();
                
                while (it.NextVisible(true))
                {
                    if (it.name == "tags")
                    {
                        it.ClearArray();
                        it.arraySize = tags.Count;
                        for (int i = 0; i < it.arraySize; i++)
                        {
                            SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                            dataPoint.stringValue = tags[i];
                        }
                        tagManager.ApplyModifiedProperties();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            _layerText=EditorGUILayout.TextField("Layer", _layerText);
            if (GUILayout.Button("导入"))
            {
                string[] layers = _layerText.Split(',');
                for (int i = 0; i < layers.Length; i++)
                {
                    AddLayer(layers[i]);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        void AddTag(string tag)
        {
            if (!IsHasTag(tag))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty it = tagManager.GetIterator();
                while (it.NextVisible(true))
                {
                    if (it.name == "tags")
                    {
                        for (int i = 0; i < it.arraySize; i++)
                        {
                            SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                            Debug.Log(dataPoint.stringValue);
                            dataPoint.stringValue = tag;
                        }
                        tagManager.ApplyModifiedProperties();
                    }
                }
            }
        }
        void AddLayer(string layer)
        {
            if (!IsHasLayer(layer))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty it = tagManager.GetIterator();
                while (it.NextVisible(true))
                {
                    Debug.Log(it.name);
                    if (it.name.StartsWith("User Layer"))
                    {
                        
                        if (it.type == "string")
                        {
                            if (string.IsNullOrEmpty(it.stringValue))
                            {
                                it.stringValue = layer;
                                tagManager.ApplyModifiedProperties();
                                return;
                            }
                        }
                    }
                }
            }
        }
        bool IsHasTag(string tag)
        {
            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
            {
                if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
                    return true;
            }
            return false;
        }

        bool IsHasLayer(string layer)
        {
            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
            {
                if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                    return true;
            }
            return false;
        }
    }
}
