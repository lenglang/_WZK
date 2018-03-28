using UnityEditor;
using UnityEngine;
using System.IO;
//using Babybus.Uno;
public class ShaderRename
{
    [MenuItem("Assets/替换Shader名")]
    static void Rename()
    {
        UnityEngine.Object obj = Selection.activeObject;
        string directoryPath = AssetDatabase.GetAssetPath(obj);
        GetShaders(directoryPath.Replace("Assets/", ""));
    }
    public static void GetShaders(string assetsPath)
    {
        string[] shadersPath = Directory.GetFiles(Application.dataPath + "/" + assetsPath, "*.shader", SearchOption.AllDirectories);
        bool replace = false;
        string str;
        for (int i = 0; i < shadersPath.Length; i++)
        {
            string[] scriptAllLines = File.ReadAllLines(shadersPath[i]);
            replace = false;
            for (int j = 0; j < scriptAllLines.Length; j++)
            {
                if (scriptAllLines[j].Contains("Shader")&& scriptAllLines[j].Contains("\"")&& replace==false)
                {
                    //str = "Shader " + "\"" + "Babybus/"+ UnoCfg.App.CodeName()+ i+ "\"";
                    str = "Shader " + "\"" + "Babybus/Test"+i + "\"";
                    if (scriptAllLines[j].Contains("{"))str+="{";
                    scriptAllLines[j] = str;
                    replace = true;//已替换
                }
            }
            File.WriteAllLines(shadersPath[i], scriptAllLines);
        }
        AssetDatabase.Refresh();
    }
}