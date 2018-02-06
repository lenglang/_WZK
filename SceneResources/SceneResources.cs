using UnityEngine;
namespace WZK
{
    public class SceneResources : MonoBehaviour
    {
        private static SceneResources _instance;
        public static SceneResources Instance
        {
            get { return _instance; }
        }
        private void Awake()
        {
            _instance = this;
        }
        private void OnDestroy()
        {
            _instance = null;
        }
        [Header("场景资源配置")]
        public ResourcesConfig _resourcesConfig;
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">资源名|资源路径|资源名+后缀</param>
        /// <returns></returns>
        public T Load<T>(string name) where T : Object
        {
            return (T)_resourcesConfig._objectList.Find(n => n._assetPath.Contains(name))._object;
        }
    }
}