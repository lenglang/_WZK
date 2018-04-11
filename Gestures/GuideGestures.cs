using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WZK;
namespace WZK
{
    public class GuideGestures : MonoBehaviour
    {
        // Use this for initialization
        [Header("移动半径")]
        public float _radius = 0.1f;
        /// <summary>
        /// 可移动的层级（单独设定一个层级）
        /// </summary>
        [Header("可移动的层级（单独设定一个层级）")]
        public int _layerIndex = 1;
        private List<Vector3> _positionList = new List<Vector3>();
        /// <summary>
        /// 边缘是否出现了循环
        /// </summary>
        private bool _loop = false;
        private Vector3 _currentPosition;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 1 << 0))
                {
                    if (Vector3.Distance(transform.position, hit.point) > 0.1f && _loop == false)
                    {
                        transform.LookAt(hit.point);
                        transform.position = GetPosition(hit.point);
                    }
                    if (_loop && _currentPosition != hit.point)
                    {
                        _loop = false;
                    }
                    _currentPosition = hit.point;
                }
            }
        }
        private Vector3 GetPosition(Vector3 hitPosition)
        {
            Vector3 p1 = Vector3.zero;
            Vector3 p2 = Vector3.zero;
            float maxDis = 1000f;
            float dis = 0;
            for (int i = 0; i < 36; i++)
            {
                p1.x = transform.position.x + _radius * Mathf.Cos(i * 10f);
                p1.z = transform.position.z + _radius * Mathf.Sin(i * 10f);
                dis = Vector3.Distance(p1, hitPosition);
                if (dis < maxDis && GameObjectExtension.IsRayHit3D(p1, null, _layerIndex))
                {
                    maxDis = dis;
                    p2 = p1;
                }
            }
            _positionList.Add(p2);
            if (_positionList.Count > 1 && Vector3.Distance(_positionList[0], p2) < _radius / 5)
            {
                _loop = true;
            }
            if (_positionList.Count >= 3)
            {
                _positionList.RemoveAt(0);
            }
            return p2;
        }
    }
}