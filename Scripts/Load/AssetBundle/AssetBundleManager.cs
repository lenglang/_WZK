using System;
using System.IO;
using UnityEngine;
namespace WZK
{
    public class AssetBundleManager : MonoBehaviour
    {
        private static AssetBundleManager _instance;
        public static AssetBundleManager Instance
        {
            get
            {

                if (_instance == null)
                {
                    _instance = (new GameObject("AssetBundleManager")).AddComponent<AssetBundleManager>();
                }
                return _instance;
            }
        }
        private Action<AssetBundle> _completeAction=null;
        private string _fileName = "";
        /// <summary>
        /// 加载AssetBundle
        /// </summary>
        /// <param name="fullPath">文件路径</param>
        /// <param name="complete">完成回调</param>
        public void Load(string fullPath,Action<AssetBundle> complete=null)
        {
            _completeAction = complete;
            _fileName = Path.GetFileName(fullPath);
            this.CreateAssetBundleFromFile(fullPath, OnComplete);
        }
        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="ab"></param>
        private void OnComplete(AssetBundle ab)
        {
            MemoryPrefs.SetObject(_fileName, ab);
            if (_completeAction != null) _completeAction(ab);
        }
    }
}
