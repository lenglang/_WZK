using System.Collections.Generic;
using UnityEngine;
namespace WZK
{
    public class MyResources : Singleton<MyResources>
    {
        [Header("ScriptableObject共享资源")]
        public ResourcesScriptableObject _resourcesScriptableObject;
        [Header("Sprite集合")]
        public List<Sprite> _spriteList = new List<Sprite>();
        [Header("Texture集合")]
        public List<Texture> _textureList = new List<Texture>();
        protected override void Awake()
        {
            base.Awake();
        }
        public T Load<T>(string path) where T : Object
        {
            Debug.Log("合包资源:" + path);
            path = "Assets/合包资源/" + path;
            return (T)_resourcesScriptableObject.GetObject(path);
        }
        public Sprite LoadSprite(string path)
        {
            Debug.Log("Sprite:" + path);
            string name = path.Substring(path.LastIndexOf("/") + 1);
            Debug.Log(name);
            return _spriteList.Find(n => n.name == name);
        }
        public Texture LoadTexture(string path)
        {
            Debug.Log("Texture:" + path);
            string name = path.Substring(path.LastIndexOf("/") + 1);
            return _textureList.Find(n => n.name == name);
        }
        public List<T> LoadAll<T>(string path) where T : Object
        {
            Debug.Log("LoadAll:" + path);
            List<T> list = new List<T>();
            for (int i = 0; i < _resourcesScriptableObject._objectList.Count; i++)
            {
                if (_resourcesScriptableObject._objectList[i]._assetPath.Contains(path))
                {
                    list.Add((T)_resourcesScriptableObject._objectList[i]._object);
                }
            }
            return list;
        }
    }
}