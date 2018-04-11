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
        [Header("画布")]
        public Canvas _canvas;
        public Action<GameObject, DragGestures2D> _onDownBefore;//按下前委托动作(用来先设置偏移量)
        public Action<GameObject, DragGestures2D> _onDown;//按下委托动作
        public Action<GameObject, DragGestures2D> _onBeginDrag;//开始拖拽委托动作
        public Action<GameObject, DragGestures2D> _onDrag;//拖拽中委托动作
        public Action<GameObject, DragGestures2D> _onEndDrag;//结束拖拽委托动作
        private bool _isDown = false;//是否按下
        private int _defaultId = 100;
        private int _currentPointerId = 100;//当前手指ID
        private PointerEventData _pointerEventData;
        private Vector3 _offset=Vector3.zero;//偏移量
        private bool _isOffset=true;//是否偏移，false即不偏移位置会对准到点击位置
        public static DragGestures2D Get(GameObject go)
        {
            DragGestures2D listener = go.GetComponent<DragGestures2D>();
            if (listener == null) listener = go.AddComponent<DragGestures2D>();
            return listener;
        }
        public DragGestures2D SetCanvas(Canvas canvas)
        {
            _canvas = canvas;
            return this;
        }
        /// <summary>
        /// 设置是否偏移
        /// </summary>
        /// <param name="isOffset"></param>
        /// <returns></returns>
        public DragGestures2D SetIsOffset(bool isOffset)
        {
            _isOffset = isOffset;
            return this;
        }
        public PointerEventData GetPointerEventData()
        {
            return _pointerEventData;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_currentPointerId == _defaultId) _currentPointerId = eventData.pointerId;
            if (_currentPointerId != eventData.pointerId) return;
            if (_isOffset) { _offset = transform.position - GetPosition(eventData); }
            else { UpdatePosition(eventData); }
            _pointerEventData = eventData;
            _isDown = true;
            if (_onDown != null) _onDown(gameObject,this);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_currentPointerId != eventData.pointerId) return;
            UpdatePosition(eventData);
            if (_onBeginDrag != null) _onBeginDrag(gameObject,this);
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (_currentPointerId != eventData.pointerId) return;
            UpdatePosition(eventData);
            if (_onDrag != null) _onDrag(gameObject,this);
        }
        public void UpdatePosition(PointerEventData eventData)
        {
            transform.position = GetPosition(eventData);
            if (_isOffset) transform.position += _offset;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (_currentPointerId != eventData.pointerId) return;
            _isDown = false;
            _currentPointerId = _defaultId;
            if (_onEndDrag != null) _onEndDrag(gameObject,this);
        }
        public Vector3 GetPosition(PointerEventData eventData)
        {
            Vector2 pos;
            pos = eventData.position;
            if (_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return RectTransformUtility.PixelAdjustPoint(pos, transform, _canvas);
            }
            else
            {
                return _canvas.worldCamera.ScreenToWorldPoint(eventData.position);
            }
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

