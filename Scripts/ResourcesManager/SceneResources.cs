using System.Collections.Generic;
using UnityEngine;
namespace WZK
{
    public class SceneResources : Singleton<SceneResources>
    {
        //[Header("其他合集")]
        //public List<Object> _otherList=new List<Object>();
        //[Header("Sprite集合")]
        //public List<Sprite> _spriteList = new List<Sprite>();
        //[Header("Texture集合")]
        //public List<Texture> _textureList = new List<Texture>();
        //protected override void Awake()
        //{
        //    base.Awake();
        //}
        //public T Load<T>(string path) where T : Object
        //{
        //    path = "Assets/" + path;
        //    return (T)_resourcesScriptableObject.GetObject(path);
        //}
        //public Sprite LoadSprite(string path)
        //{
        //    Debug.Log("Sprite:" + path);
        //    return _spriteList.Find(n => n.name == name);
        //}
        //public Texture LoadTexture(string path)
        //{
        //    Debug.Log("Texture:" + path);
        //    return _textureList.Find(n => n.name == name);
        //}
        //public List<T> LoadAll<T>(string path) where T : Object
        //{
        //    Debug.Log("LoadAll:" + path);
        //    List<T> list = new List<T>();
        //    for (int i = 0; i < _resourcesScriptableObject._objectList.Count; i++)
        //    {
        //        if (_resourcesScriptableObject._objectList[i]._assetPath.Contains(path))
        //        {
        //            list.Add((T)_resourcesScriptableObject._objectList[i]._object);
        //        }
        //    }
        //    return list;
        //}
    }
}