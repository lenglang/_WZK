using System.Collections.Generic;
using UnityEngine;
namespace WZK
{
    [CreateAssetMenu(fileName = "xxx场景资源", menuName = "创建场景序列化资源")]
    public class ResourcesConfig : ScriptableObject
    {
        public List<Config> _objectList = new List<Config>();
        public List<string> _choseExtensionList = new List<string>();//扩展名
        public int _showMin = 1;
        public int _showMax = 10000;
        [System.Serializable]
        public class Config
        {
            public Object _object;//物体
            public string _assetPath;//Asset下路径
            public Config(Object obj = null, string assetPath = "")
            {
                _object = obj;
                _assetPath = assetPath;
            }
        }
    }
}
