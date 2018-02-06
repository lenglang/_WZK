using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
namespace WZK
{
    public class RepairShader : MonoBehaviour
    {
        private float currentTime;
#if UNITY_EDITOR
        void Update()
        {
            if (Time.realtimeSinceStartup - currentTime > 2f)
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