using UnityEngine;
namespace WZK
{
    /// <summary>
    /// 浮动效果
    /// </summary>
    public class Floating : MonoBehaviour
    {
        [Header("x轴移动距离")]
        public float _xMoveDis = 0;
        [Header("x轴移动弧度")]
        public float _xMoveRadian = 0f;
        private float _xRadian = 0;//弧度增量
        [Header("y轴移动距离")]
        public float _yMoveDis = 0;
        [Header("x轴移动弧度")]
        public float _yMoveRadian = 0f;
        private float _yRadian = 0;//弧度增量
        [Header("z轴移动距离")]
        public float _zMoveDis = 0;
        [Header("z轴移动弧度")]
        public float _zMoveRadian = 0f;
        private float _zRadian = 0;//弧度增量
        private Vector3 _originTransform;//原点
        void Start()
        {
            _originTransform = transform.position;
        }
        private void FixedUpdate()
        {
            _xRadian += _xMoveRadian;
            _yRadian += _yMoveRadian;
            _zRadian += _zMoveRadian;
            transform.position = _originTransform + new Vector3(Mathf.Cos(_xRadian) * _xMoveDis, Mathf.Cos(_yRadian) * _yMoveDis, Mathf.Cos(_zRadian) * _zMoveDis);
        }
    }
}