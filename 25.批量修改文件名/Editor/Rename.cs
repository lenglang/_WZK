
using System.IO;
using UnityEditor;
using UnityEngine;
namespace WZK
{
    public class Rename : Editor
    {
        private static string _folderName = "";//文件夹名
        private static string _assetPath = "";//文件项目路径
        private static string _folderPath = "";//文件电脑路径
        [MenuItem("WZK/更改名字/将文件夹下文件名含prefab的替换成文件夹名")]
        public static void Rename1()
        {
            FileInfo[] files = GetFiles();
            if (files == null || files.Length == 0) return;
            string oldpath = "";
            string newpath = "";
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".prefab") == false) continue;
                oldpath = "";
                newpath = "";
                #region 因需要替换的名正好是后缀，所以才这么麻烦去处理
                oldpath = files[i].FullName;
                newpath = oldpath.Replace(files[i].Extension, "");
                newpath = newpath.Replace("prefab", _folderName);
                newpath += files[i].Extension;
                #endregion
                if (System.IO.File.Exists(oldpath))
                {
                    Debug.Log(newpath);
                    System.IO.File.Move(oldpath, newpath);
                }
            }
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 获取文件夹里文件
        /// </summary>
        public static FileInfo[] GetFiles()
        {
            Object obj = Selection.activeObject;
            if (obj==null||obj.GetType().ToString() != "UnityEditor.DefaultAsset") return null;
            _assetPath = AssetDatabase.GetAssetPath(obj);
            _folderName = _assetPath.Substring(_assetPath.LastIndexOf("/") + 1);
            _folderPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/") + 1) + _assetPath;
            DirectoryInfo direction = new DirectoryInfo(_folderPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            return files;
        }
    }
}
