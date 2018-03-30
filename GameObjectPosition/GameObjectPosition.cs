using System.Collections.Generic;
using UnityEngine;
namespace WZK
{
    public class GameObjectPosition : MonoBehaviour
    {
        public List<TransformInformation> _list = new List<TransformInformation>();
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation GetInformation(string desc)
        {
            for (int j = 0; j < _list.Count; j++)
            {
                if (_list[j]._desc == desc) return _list[j];
            }
            return null;
        }
        /// <summary>
        /// 获取局部位置坐标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Vector3 GetLocalPosition(string desc)
        {
            return GetInformation(desc)._localPosition;
        }
        /// <summary>
        /// 获取世界位置坐标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Vector3 GetPosition(string desc)
        {
            return GetInformation(desc)._position;
        }
        /// <summary>
        /// 获取旋转角度
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Quaternion GetRotation(string desc)
        {
            return GetInformation(desc)._rotation;
        }
        public Vector3 GetAngle(string desc)
        {
            return GetInformation(desc)._angle;
        }
        /// <summary>
        /// 获取缩放大小
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Vector3 GetScale(string desc)
        {
            return GetInformation(desc)._localScale;
        }
        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetInformation(GameObject obj, string desc)
        {
            TransformInformation information = GetInformation(desc);
            Transform tf = obj.GetComponent<Transform>();
            tf.localPosition = information._localPosition;
            tf.rotation = information._rotation;
            tf.localScale = information._localScale;
            return information;
        }
        /// <summary>
        /// 将自身设定到某个位置
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetInformation(string desc)
        {
            TransformInformation information = GetInformation(desc);
            Transform tf = gameObject.GetComponent<Transform>();
            tf.position = information._position;
            tf.rotation = information._rotation;
            tf.localScale = information._localScale;
            return information;
        }
        /// <summary>
        /// 将自身设定到某个位置
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetLocalInformation(string desc)
        {
            TransformInformation information = GetInformation(desc);
            Transform tf = gameObject.GetComponent<Transform>();
            tf.localPosition = information._localPosition;
            tf.rotation = information._rotation;
            tf.localScale = information._localScale;
            return information;
        }
        /// <summary>
        /// 设置局部位置坐标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetLocalPosition(GameObject obj, string desc)
        {
            TransformInformation information = GetInformation(desc);
            obj.GetComponent<Transform>().localPosition = information._localPosition;
            return information;
        }
        /// <summary>
        /// 设置局部位置坐标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetLocalPosition(string desc)
        {
            TransformInformation information = GetInformation(desc);
            transform.localPosition = information._localPosition;
            return information;
        }
        /// <summary>
        /// 设置世界位置坐标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetPosition(GameObject obj, string desc)
        {
            TransformInformation information = GetInformation(desc);
            obj.GetComponent<Transform>().position = information._position;
            return information;
        }
        /// <summary>
        /// 设置世界位置坐标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetPosition(string desc)
        {
            TransformInformation information = GetInformation(desc);
            transform.position = information._position;
            return information;
        }
        /// <summary>
        /// 设置旋转角度
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetRotation(GameObject obj, string desc)
        {
            TransformInformation information = GetInformation(desc);
            obj.GetComponent<Transform>().rotation = information._rotation;
            return information;
        }
        /// <summary>
        /// 设置旋转角度
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetRotation(string desc)
        {
            TransformInformation information = GetInformation(desc);
            transform.rotation = information._rotation;
            return information;
        }
        /// <summary>
        /// 设置缩放大小
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetScale(GameObject obj, string desc)
        {
            TransformInformation information = GetInformation(desc);
            obj.GetComponent<Transform>().localScale = information._localScale;
            return information;
        }
        /// <summary>
        /// 设置缩放大小
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public TransformInformation SetScale(string desc)
        {
            TransformInformation information = GetInformation(desc);
            transform.localScale = information._localScale;
            return information;
        }
    }
    [System.Serializable]
    public class TransformInformation
    {
        public string _desc = "初始";
        public Vector3 _localPosition;
        public Vector3 _position;
        public Quaternion _rotation;
        public Vector3 _localScale;
        public Vector3 _scale;
        public Vector3 _angle;
        public GameObject _gameObject;
    }
}
