using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace WZK
{
    public class ShaderEditor : Editor
    {
        //[MenuItem("Tools/一键将")]
        //static void ShaderSoftEdgeUnlit()
        //{
        //    Shader shader = Shader.Find("Unlit/Transparent");
        //    string[] tempMaterialsPath = AssetDatabase.GetAllAssetPaths();
        //    List<Material> tempMaterials = new List<Material>();
        //    for (int i = 0; i < tempMaterialsPath.Length; i++)
        //    {
        //        string ext = Path.GetExtension(tempMaterialsPath[i]);
        //        if (ext != ".mat")
        //        {
        //            continue;
        //        }
        //        tempMaterials.Add(AssetDatabase.LoadAssetAtPath(tempMaterialsPath[i], typeof(Material)) as Material);
        //    }

        //    if (tempMaterials.Count != 0)
        //    {
        //        for (int i = 0; i < tempMaterials.Count; i++)
        //        {
        //            if (tempMaterials[i] == null)
        //            {
        //                continue;
        //            }
        //            if (tempMaterials[i].shader.name == "Legacy Shaders/Diffuse")
        //            {
        //                if(tempMaterials[i].name=="mmdgx_dj_bandeng_yy(chunjie)")
        //                Debug.Log(tempMaterials[i].name);
        //                //tempMaterials[i].shader = shader;
        //            }
        //        }
        //    }
        //}
    }
}
