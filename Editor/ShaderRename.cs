using UnityEditor;
using UnityEngine;
using System.IO;
public class ShaderRename : EditorWindow
{
    [MenuItem("Assets/WZK/批量替换Shader名")]
    static void Rename()
    {
        GetWindow<ShaderRename>().Show();
    }
    private static string _name;
    private void OnGUI()
    {
        _name = EditorGUILayout.TextField("shader名", _name);
        if (GUILayout.Button("替换") && string.IsNullOrEmpty(_name) == false)
        {
            int index = -1;
            if (EditorUtility.DisplayDialog("提示", "是否批量处理", "是", "否"))
            {
                index = 0;
            }
            Object[] SelectedAsset = Selection.GetFiltered(typeof(object), SelectionMode.DeepAssets);
            for (int i = 0; i < SelectedAsset.Length; i++)
            {
                if (SelectedAsset[i].GetType().ToString() == "UnityEngine.Shader")
                {
                    if (index>=0)
                    {
                        ChangeShader(AssetDatabase.GetAssetPath(SelectedAsset[i]),index);
                        index++;
                        Debug.Log(index + ":" + AssetDatabase.GetAssetPath(SelectedAsset[i]));
                    }
                    else
                    {
                        ChangeShader(AssetDatabase.GetAssetPath(SelectedAsset[i]));
                    }
                }
            }
            AssetDatabase.Refresh();
        }
    }
    private static void ChangeShader(string path, int index = -1)
    {
        string[] scriptAllLines = File.ReadAllLines(path);
        bool replace = false;
        string str;
        for (int j = 0; j < scriptAllLines.Length; j++)
        {
            if (scriptAllLines[j].Contains("Shader") && scriptAllLines[j].Contains("\"") && replace == false)
            {
                str = "Shader " + "\"" + _name + "\"";
                if (index != -1) str = "Shader " + "\"" + _name + index + "\"";
                if (scriptAllLines[j].Contains("{")) str += "{";
                scriptAllLines[j] = str;
                replace = true;//已替换
            }
        }
        File.WriteAllLines(path, scriptAllLines);
    }
}