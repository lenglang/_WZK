using System.Collections.Generic;
using UnityEngine;
namespace WZK
{
    [CreateAssetMenu(fileName = "资源序列化", menuName = "创建序列化资源")]
    public class ResourcesScriptableObject : ScriptableObject
    {
        public List<Config> _objectList = new List<Config>();
        public List<string> _choseExtensionList = new List<string>();//扩展名
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="path">asset下路径</param>
        /// <returns></returns>
        public Object GetObject(string path)
        {
            return _objectList.Find(n => n._assetPath == path)._object;
        }
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
