using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
namespace WZK
{
    public class RepairShader : MonoBehaviour
    {
        private static RepairShader _instance=null;
        /// <summary>
        /// 创建
        /// </summary>
        public static  void Create()
        {
            if (_instance == null)
            {
                _instance = new GameObject("修复Shader").AddComponent<RepairShader>();
                DontDestroyOnLoad(_instance.gameObject);
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        public static void Remove()
        {
            Destroy(_instance.gameObject);
            _instance = null;
        }
        private float currentTime;
#if UNITY_EDITOR
        void Update()
        {
            if (Time.realtimeSinceStartup - currentTime > 0.5f)
            {
                currentTime = Time.realtimeSinceStartup;
                BackShader();
            }
        }
        private static void BackShader()
        {
            var renderers = Resources.FindObjectsOfTypeAll<Renderer>();
            if (renderers == null || renderers.Length == 0)
                return;

            foreach (var renderer in renderers)
            {
                if (AssetDatabase.GetAssetPath(renderer) != "")
                    continue;

                foreach (var sharedMaterial in renderer.sharedMaterials)
                {
                    if (sharedMaterial != null)
                        sharedMaterial.shader = Shader.Find(sharedMaterial.shader.name);
                }
            }
            var graphics = Resources.FindObjectsOfTypeAll<Graphic>();
            if (graphics == null || graphics.Length == 0) return;
            foreach (var graphic in graphics)
            {
                if (AssetDatabase.GetAssetPath(graphic) != "")
                    continue;
                graphic.material.shader = Shader.Find(graphic.material.shader.name);
            }
        }
#endif
    }
}