using UnityEditor;
using UnityEngine;
namespace WZK
{
    public class TagAndLayer : Editor
    {
        [MenuItem("WZK/TagAndLayer/导出")]
        static void ExportTagAndLayer()
        {
            AddString("Tag:" + "\n", UnityEditorInternal.InternalEditorUtility.tags,7);
            AddString("Layer:" + "\n", UnityEditorInternal.InternalEditorUtility.layers,5);
        }
        static void AddString(string name, string[] strs,int limit)
        {
            string str = name;
            for (int i = limit; i < strs.Length; i++)
            {
                str += strs[i];
                if (i != strs.Length - 1) str += ",";
            }
            Debug.Log(str);
        }
        [MenuItem("WZK/TagAndLayer/导入")]
        static void ImportTagAndLayer()
        {
            EditorWindow.GetWindow<ImportTagAndLayerWindow>(false, "导入TagAndLayer");
        }
    }
}
