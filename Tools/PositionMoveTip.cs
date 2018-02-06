using UnityEngine;
namespace WZK
{
    public class PositionMoveTip : MonoBehaviour
    {
        public enum Direction
        {
            从下到上,
            从上到下,
            从右到左,
            从左到右,
            从前到后,
            从后到前
        }
        [Header("开始X")]
        public float _startX = 0;
        [Header("结束X")]
        public float _endX = 0;
        [Header("开始Y")]
        public float _startY = 0;
        [Header("结束Y")]
        public float _endY = 0;
        [Header("移动速度")]
        public float _speed = 0;
        [Header("移动方向")]
        public Direction _direction;
        private float _times = 0;//次数
        private Vector3 _currentPosition;//当前位置
        void Start()
        {
            transform.localPosition = new Vector3(_startX, _startY, transform.position.z);
        }
        void FixedUpdate()
        {
            _times++;
            if (_times < 2) return;
            _times = 0;
            _currentPosition = this.transform.localPosition;
            switch (_direction)
            {
                case Direction.从上到下:
                    _currentPosition.y += _speed;
                    if (_currentPosition.y < _endY) _currentPosition.y = _startY;
                    break;
                case Direction.从下到上:
                    _currentPosition.y += _speed;
                    if (_currentPosition.y > _endY) _currentPosition.y = _startY;
                    break;
                case Direction.从左到右:
                    _currentPosition.x += _speed;
                    if (_currentPosition.x > _endX) _currentPosition.x = _startX;
                    break;
                case Direction.从右到左:
                    _currentPosition.x += _speed;
                    if (_currentPosition.x < _endX) _currentPosition.x = _startX;
                    break;
                default:
                    break;
            }
            transform.localPosition = _currentPosition;
        }
    }
}
