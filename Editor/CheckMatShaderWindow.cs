using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
///  用来检查材质换了shader之后 对原来属性仍有引用的的工具
/// </summary>
public class CheckMatShaderWindow : EditorWindow
{

    [MenuItem("Tools/检查/检查材质和shader参数不符的材质")]
    private static void CheckMat()
    {
        GetWindow<CheckMatShaderWindow>().Show();
        Check();
    }

    [MenuItem("Tools/检查/检查Hidden-InternalErrorShader的Material")]
    private static void FindErrorShader()
    {
        GetWindow<CheckMatShaderWindow>().Show();
        FindHiddenShader();
    }
    private static void FindHiddenShader()
    {
        flag = "ErrorShader";
        res.Clear();
        var mats = AssetDatabase.GetAllAssetPaths().Where(s => s.EndsWith("mat")).Select(s => AssetDatabase.LoadAssetAtPath<Material>(s));
        foreach (var item in mats)
        {
            if (item.shader.name.Contains("Error"))
            {
                res.Add(item, null);
            }
        }
        EditorUtility.DisplayDialog("", "就绪", "OK");
    }

    private static string flag = "";
    private static Dictionary<Material, Dictionary<string, UnityEngine.Object>> res = new Dictionary<Material, Dictionary<string, UnityEngine.Object>>();

    private static void Check()
    {
        flag = "Check";
        res.Clear();
        var allfiles = Directory.GetFiles("Assets/", "*.mat", SearchOption.AllDirectories);
        foreach (var file in allfiles)//遍历所有材质
        {
            Material tempMat = AssetDatabase.LoadAssetAtPath<Material>(file);
            string[] temp = GetTextureProperyOfShader(tempMat.shader);
            var resul = GetTextureProperyOfMaterial(tempMat).Where(s => !temp.Contains(s.Key)).ToDictionary(s => s.Key, s => s.Value);
            if (resul.Count != 0) res.Add(tempMat, resul);
        }
        EditorUtility.DisplayDialog("一共查找了" + allfiles.Length + "个材 质球", "就绪", "OK");
    }
    ///***********************************核心**********************************
    /// <summary>
    /// 获取一个材质的贴图属性 贴图不为空的属性
    /// </summary>
    /// <param name="m"></param>
    private static Dictionary<string, UnityEngine.Object> GetTextureProperyOfMaterial(Material m)
    {
        Dictionary<string, UnityEngine.Object> dic = new Dictionary<string, UnityEngine.Object>();
        SerializedObject so = new SerializedObject(m);
        var iter = so.GetIterator();
        while (iter.NextVisible(true))
        {
            if (iter.propertyPath.Contains("m_TexEnvs"))
            {
                for (int i = 0; i < iter.FindPropertyRelative("Array").FindPropertyRelative("size").intValue; i++)
                {
                    UnityEngine.Object o = iter.FindPropertyRelative("Array").FindPropertyRelative("data[" + i + "]").FindPropertyRelative("second").FindPropertyRelative("m_Texture").objectReferenceValue;
                    if (o != null)
                        dic.Add(iter.FindPropertyRelative("Array").FindPropertyRelative("data[" + i + "]").FindPropertyRelative("first").FindPropertyRelative("name").stringValue, o);
                }
                break;
            }
        }
        return dic;
    }
    /// <summary>
    /// 获取一个shader的贴图属性
    /// </summary>
    /// <param name="s"></param>
    /// <returns>结果</returns>
    private static string[] GetTextureProperyOfShader(Shader s)
    {
        List<string> t = new List<string>();
        for (int i = 0; i < ShaderUtil.GetPropertyCount(s); i++)
        {
            if (ShaderUtil.GetPropertyType(s, i) == ShaderUtil.ShaderPropertyType.TexEnv)
            {
                t.Add(ShaderUtil.GetPropertyName(s, i));
            }
        }
        return t.ToArray();
    }
    ///******************************************

    /// <summary>
    /// 清除多余的贴图属性
    /// </summary>
    private static void ClearPropery()
    {
        int count = 0;
        foreach (var item in res)
        {
            if (item.Key.shader.name.Contains("Error")) continue;
            foreach (var item2 in item.Value)
            {
                item.Key.SetTexture(item2.Key, null);
                count++;
            }
        }
        EditorUtility.DisplayDialog("完成", "一个清除" + count + "个属性", "OK");
        if (count != 0)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Check();
        }
    }
    private Vector2 scro = Vector2.zero;
    private void OnGUI()
    {
        if (flag == "Check")
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Count:" + res.Count);
            if (GUILayout.Button("清除所有多余的属性")) ClearPropery();
            EditorGUILayout.EndHorizontal();
            scro = EditorGUILayout.BeginScrollView(scro);
            EditorGUILayout.BeginVertical();
            foreach (var item in res)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(item.Key, typeof(GameObject), true, GUILayout.Width(200));
                EditorGUILayout.BeginVertical();
                foreach (var o in item.Value)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(o.Key, GUILayout.Width(o.Key.Length * 10));
                    EditorGUILayout.ObjectField(o.Value, typeof(GameObject), true, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
        else if (flag == "ErrorShader")
        {
            scro = EditorGUILayout.BeginScrollView(scro);
            EditorGUILayout.BeginVertical();
            foreach (var item in res)
            {
                EditorGUILayout.ObjectField(item.Key, typeof(GameObject), true, GUILayout.Width(200));
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
    }
}