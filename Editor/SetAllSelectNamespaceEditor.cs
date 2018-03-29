using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System;
namespace WZK
{
    public class SetAllSelectNamespaceEditor : EditorWindow
    {
        private static string _name;
        [MenuItem("Assets/WZK/批量修改名空间")]
        static void Rename()
        {
            SetAllSelectNamespaceEditor window = GetWindow<SetAllSelectNamespaceEditor>();
            
            window.titleContent.text = "批量修改名空间"; 
            window.Show();
        }
        private void OnGUI()
        {
            GUILayout.Space(50);
            _name = EditorGUILayout.TextField("命名空间名", _name);
            GUILayout.Space(50);
            if (GUILayout.Button("替换") && string.IsNullOrEmpty(_name) == false)
            {
                SetAllSelectNamespace();
            }
        }
        static void SetAllSelectNamespace()
        {
            StringBuilder stringBuilder;
            if (Selection.objects.Length == 0)
                return;
            UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(object), SelectionMode.DeepAssets);
            for (int i = 0; i < SelectedAsset.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(SelectedAsset[i]);
                if (Directory.Exists(path))
                {
                    continue;
                }
                //if ("cs" != SelectedAsset[i].name.Substring(SelectedAsset[i].name.LastIndexOf(".") + 1))
                //{
                //    continue;
                //}
                string[] scriptAllLines = File.ReadAllLines(path);
                for (int j = 0; j < scriptAllLines.Length; j++)
                {
                    if (scriptAllLines[j].Contains("namespace"))
                    {
                        if (scriptAllLines[j].Contains("{"))
                        {
                            stringBuilder = new StringBuilder();
                            stringBuilder.Append("namespace ");
                            stringBuilder.Append(_name);
                            stringBuilder.Append("{");

                            string tempstring = scriptAllLines[j];
                            int indexof = tempstring.IndexOf("{", StringComparison.Ordinal);
                            string appendNamespace = "";
                            if (indexof < tempstring.Length - 1)
                            {
                                appendNamespace = tempstring.Substring(indexof + 1,
                                    tempstring.Length - 1 - indexof);
                            }

                            stringBuilder.Append(appendNamespace);
                            scriptAllLines[j] = stringBuilder.ToString();
                            File.WriteAllLines(path, scriptAllLines);
                        }
                        else
                        {
                            scriptAllLines[j] = "namespace " + _name;
                            File.WriteAllLines(path, scriptAllLines);
                        }
                        break;
                    }
                    if (scriptAllLines[j].Contains("class") || scriptAllLines[j].Contains("CreateAssetMenu") || scriptAllLines[j].Contains("ExecuteInEditMode") || scriptAllLines[j].Contains("RequireComponent") || scriptAllLines[j].Contains("interface") || scriptAllLines[j].Contains("CustomEditor"))
                    {
                        stringBuilder = new StringBuilder();
                        stringBuilder.Append("namespace ");
                        stringBuilder.Append(_name);
                        stringBuilder.Append("{");
                        stringBuilder.Append(scriptAllLines[j]);
                        scriptAllLines[j] = stringBuilder.ToString();
                        File.WriteAllLines(path, scriptAllLines);
                        File.AppendAllText(path, "}");
                        break;
                    }
                }
            }
            AssetDatabase.Refresh();
        }
    }
}