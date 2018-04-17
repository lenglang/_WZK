using UnityEngine;
namespace WZK
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 设定层
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layer"></param>
        public static void SetLayer(this GameObject obj, int layer)
        {
            obj.layer = layer;
            if (obj.transform.childCount == 0) return;
            foreach (Transform child in obj.GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = layer;
            }
        }
        /// <summary>
        /// 获取粒子特效时长
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float ParticleSystemLength(GameObject obj)
        {
            ParticleSystem[] particleSystems = obj.GetComponentsInChildren<ParticleSystem>();
            float maxDuration = 0;
            foreach (ParticleSystem ps in particleSystems)
            {
                if (ps.emission.enabled)
                {
                    if (ps.loop)
                    {
                        return -1f;
                    }
                    float dunration = 0f;
                    if (ps.emission.rate.constantMax <= 0)
                    {
                        dunration = ps.startDelay + ps.startLifetime;
                    }
                    else
                    {
                        dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
                    }
                    if (dunration > maxDuration)
                    {
                        maxDuration = dunration;
                    }
                }
            }
            return maxDuration;
        }
        /// <summary>
        /// 从屏幕位置点（鼠标位置点、3D物体转屏幕点）发送射线是否碰撞到该物体-用于拖拽物体的碰撞检测
        /// 传值:
        /// 1.obj._pointerEventData.position;鼠标位置点
        /// 2.Vector3 p = _otherCamera.WorldToScreenPoint(obj.transform.position);物体在其他相机(好像也可以直接用第一种方式)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="position">屏幕位置点</param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsRayHit(this GameObject obj, Vector2 position, Camera camera = null,int layerIndex = 0)
        {
            Ray ray;
            if (camera == null)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("场景中缺少照射的主摄像机，将照射相机Tag设置为MainCamera或给该类_camera属性赋值照射摄像机");
                    return false;
                }
                ray = Camera.main.ScreenPointToRay(position);
            }
            else
            {
                ray = camera.ScreenPointToRay(position);
            }
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, 1 << layerIndex))
            {
                if (hit.collider.gameObject == obj)
                {
                    return true;
                }
                else
                {
                    Debug.Log(hit.collider);
                }
            }
            return false;
        }
        /// <summary>
        /// 相机到3D位置点发射线检测
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="position"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsRayHit3D(this GameObject obj, Vector3 position, Camera camera = null, int layerIndex = 0)
        {
            Ray ray;
            if (camera == null)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("场景中缺少照射的主摄像机，将照射相机Tag设置为MainCamera或给该类_camera属性赋值照射摄像机");
                    return false;
                }
                ray = new Ray(Camera.main.transform.position, position - Camera.main.transform.position);
            }
            else
            {
                ray = new Ray(camera.transform.position, position - camera.transform.position);
            }
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, 1 << layerIndex))
            {
                if (hit.collider.gameObject == obj)
                {
                    return true;
                }
                else
                {
                    Debug.Log(hit.collider);
                }
                return hit.collider.gameObject == obj;
            }
            return false;
        }
        /// <summary>
        /// 检测位置是否在合理区
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="camera"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public static bool IsRayHit3D(Vector3 position,Camera camera = null, int layerIndex = 0)
        {
            Ray ray;
            if (camera == null)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("场景中缺少照射的主摄像机，将照射相机Tag设置为MainCamera或给该类_camera属性赋值照射摄像机");
                    return false;
                }
                ray = new Ray(Camera.main.transform.position, position - Camera.main.transform.position);
            }
            else
            {
                ray = new Ray(camera.transform.position, position - camera.transform.position);
            }
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, 1 << layerIndex))
            {
                return true;
            }
            return false;
        }
    }
}
