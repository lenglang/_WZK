using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace WZK
{
    [CreateAssetMenu(fileName = "AssetBundleManager", menuName = "创建资源打包管理")]
    public class AssetBundleConfig : ScriptableObject
    {
        public  string _savePath = "";//保存路径
        public List<Config> _SelectionObjects = new List<Config>();
        [System.Serializable]
        public class Config
        {
            public bool _bool = true;//是否打包
            public Object _object;
            public string _savePath= "";
            public string _saveName = "";
        }
        public void CreateObject(string path)
        {
            Config config = new Config();
            config._savePath = path;
            _SelectionObjects.Add(config);
        }
    }
}
