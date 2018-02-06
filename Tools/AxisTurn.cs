using UnityEngine;
namespace WZK
{
    public class AxisTurn : MonoBehaviour
    {
        [Header("是否转动X轴")]
        public bool _isX = false;
        [Header("X轴转动速度")]
        public float _xSpeed = 0;
        [Header("是否转动Y轴")]
        public bool _isY = false;
        [Header("Y轴转动速度")]
        public float _ySpeed = 0;
        [Header("是否转动Z轴")]
        public bool _isZ = false;
        [Header("Z轴转动速度")]
        public float _zSpeed = 0;
        private float _times = 0;
        private void FixedUpdate()
        {
            _times++;
            if (_times < 2) return;
            _times = 0;
            if (_isX) transform.Rotate(_xSpeed, 0, 0);
            if (_isY) transform.Rotate(0, _ySpeed, 0);
            if (_isZ) transform.Rotate(0, 0,_zSpeed);
        }
    }
}
