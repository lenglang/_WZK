using UnityEditor;
using UnityEngine;
namespace WZK
{
    public class TagAndLayer : Editor
    {
        [MenuItem("TagAndLayer/导出")]
        static void ExportTagAndLayer()
        {
            AddString("Tag:" + "\n", UnityEditorInternal.InternalEditorUtility.tags);
            AddString("Layer:" + "\n", UnityEditorInternal.InternalEditorUtility.layers);
        }
        static void AddString(string name, string[] strs)
        {
            string str = name;
            for (int i = 0; i < strs.Length; i++)
            {
                str += strs[i];
                if (i != strs.Length - 1) str += ",";
            }
            Debug.Log(str);
        }
        [MenuItem("TagAndLayer/导入")]
        static void ImportTagAndLayer()
        {
            EditorWindow.GetWindow<ImportTagAndLayerWindow>(false, "导入TagAndLayer");
        }
    }
}
