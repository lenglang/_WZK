using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace WZK
{
    /// <summary>
    /// 作者-wzk
    /// 功能-UI拖拽
    /// </summary>
    [AddComponentMenu("Common/Gestures/DragGestures2D")]
    public class DragGestures2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        public Action<GameObject, DragGestures2D> _onDownBefore;//按下前委托动作(用来先设置偏移量)
        public Action<GameObject, DragGestures2D> _onDown;//按下委托动作
        public Action<GameObject, DragGestures2D> _onBeginDrag;//开始拖拽委托动作
        public Action<GameObject, DragGestures2D> _onDrag;//拖拽中委托动作
        public Action<GameObject, DragGestures2D> _onEndDrag;//结束拖拽委托动作
        private Vector3 _localPosition;//按下UI时UI的位置
        private bool _isDown = false;//是否按下
        private Vector2 _startPosition;//鼠标（手指）按下点
        private Vector2 _currentPosition;//鼠标（手指）当前点
        private int _defaultId = 100;
        private int _currentPointerId = 100;//当前手指ID
        [HideInInspector]
        public bool _isFocusClickPoint = true;//是否对焦到点击点
        [HideInInspector]
        public PointerEventData _pointerEventData;
        public void OnPointerDown(PointerEventData evenData)
        {
            if (_currentPointerId == _defaultId) _currentPointerId = evenData.pointerId;
            if (_currentPointerId != evenData.pointerId) return;
            _pointerEventData = evenData;
            if (_isFocusClickPoint) transform.position = evenData.pressPosition;
            _isDown = true;
            _localPosition = this.transform.localPosition;
            _startPosition = evenData.position;
            if (_onDown != null) _onDown(gameObject,this);
        }
        /// <summary>
        /// 对焦点
        /// </summary>
        /// <param name="p"></param>
        public void FocusPosition(Vector3 p)
        {
            transform.position = p;
        }
        public void OnBeginDrag(PointerEventData evenData)
        {
            if (_currentPointerId != evenData.pointerId) return;
            UpdatePosition(evenData);
            if (_onBeginDrag != null) _onBeginDrag(gameObject,this);
        }
        public void OnDrag(PointerEventData evenData)
        {
            if (_currentPointerId != evenData.pointerId) return;
            UpdatePosition(evenData);
            if (_onDrag != null) _onDrag(gameObject,this);
        }
        public void UpdatePosition(PointerEventData evenData)
        {
            _pointerEventData = evenData;
            _currentPosition = evenData.position;
            this.transform.localPosition = new Vector2(_localPosition.x + _currentPosition.x - _startPosition.x, _localPosition.y + _currentPosition.y - _startPosition.y);
        }
        public void OnPointerUp(PointerEventData evenData)
        {
            if (_currentPointerId != evenData.pointerId) return;
            _isDown = false;
            _currentPointerId = _defaultId;
            if (_onEndDrag != null) _onEndDrag(gameObject,this);
        }
        void OnApplicationPause(bool isPause)
        {
            if (isPause)
            {
                //游戏暂停-缩到桌面的时候触发
            }
            else
            {
                //游戏开始-回到游戏的时候触发
                if (_onEndDrag != null && _isDown)
                {
                    _currentPointerId = _defaultId;
                    _isDown = false;
                    _onEndDrag(gameObject, this);
                }
            }
        }
    }
}

