using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace WZK
{
    /// <summary>
    /// 作者-wzk
    /// 功能-3D物体拖拽
    /// 使用说明-直接以组件形式添加到物体上设置_camera来指定照射相机，场景需添加EventSystem事件系统，照射相机需添加物理射线，3D物体需有Collider相关组件
    /// </summary>
    [AddComponentMenu("Common/Gestures/DragGestures3D")]
    public class DragGestures3D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        public Action<GameObject, DragGestures3D> _onDownBefore;//按下前委托动作(用来先设置偏移量)
        public Action<GameObject, DragGestures3D> _onDown;//按下委托动作
        public Action<GameObject, DragGestures3D> _onBeginDrag;//开始拖拽委托动作
        public Action<GameObject, DragGestures3D> _onDrag;//拖拽中委托动作
        public Action<GameObject, DragGestures3D> _onEndDrag;//结束拖拽委托动作
        [HideInInspector]
        public PointerEventData _pointerEventData;//事件数据
        [HideInInspector]
        public Camera _camera;//照射相机
        [HideInInspector]
        public Rect _rectEdge = new Rect(0, 0, 0, 0);//边缘
        private Camera _currentCamera;//当前相机
        private Vector3 _offset;//偏移点
        private Vector3 _lastPosition;//上一个位置
        private Vector3 _screenSpace;//屏幕坐标
        private bool _isDown = false;//是否按下
        private Transform _moveOutJudgePoint;//移动出判断点
        private  int _defaultId = 100;
        private  int _currentPointerId = 100;//当前手指ID
        private void Awake()
        {
            _moveOutJudgePoint = transform.FindChild("移出判断点");
        }
        /// <summary>
        /// 设置偏移
        /// </summary>
        public void SetOffset(Vector3 offset = default(Vector3))
        {
            _offset = offset;
            _screenSpace = GetCamera().WorldToScreenPoint(transform.position);
            transform.position = GetCamera().ScreenToWorldPoint(new Vector3(_pointerEventData.position.x, _pointerEventData.position.y, _screenSpace.z)) + _offset;
        }
        public void OnPointerDown(PointerEventData evenData)
        {
            if (_currentPointerId==_defaultId) _currentPointerId = evenData.pointerId;
            if (_currentPointerId != evenData.pointerId) return;
            _isDown = true;
            _pointerEventData = evenData;
            if (_onDownBefore != null) _onDownBefore(gameObject,this);
            _screenSpace = GetCamera().WorldToScreenPoint(transform.position);
            _offset = transform.position - GetCamera().ScreenToWorldPoint(new Vector3(evenData.position.x, evenData.position.y, _screenSpace.z));
            if (_onDown != null) _onDown(gameObject,this);
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
        public void OnPointerUp(PointerEventData evenData)
        {
            if (_currentPointerId != evenData.pointerId) return;
            _currentPointerId = _defaultId;
            _isDown = false;
            if (_onEndDrag != null) _onEndDrag(gameObject, this);
        }
        private void UpdatePosition(PointerEventData evenData)
        {
            _pointerEventData = evenData;
            _lastPosition = this.transform.position;
            Vector3 curScreenSpace = new Vector3(evenData.position.x, evenData.position.y, _screenSpace.z);
            Vector3 v3;
            Vector3 CurPosition = GetCamera().ScreenToWorldPoint(curScreenSpace) + _offset;
            transform.position = CurPosition;
            if (_moveOutJudgePoint != null)
            {
                v3 = Camera.main.WorldToScreenPoint(_moveOutJudgePoint.position);
                if (v3.x > Screen.width - _rectEdge.x || v3.x < _rectEdge.width || v3.y > Screen.height - _rectEdge.y || v3.y < _rectEdge.height)
                {
                    this.transform.position = _lastPosition;
                }
            }
        }
        /// <summary>
        /// 获取相机
        /// </summary>
        /// <returns></returns>
        private Camera GetCamera()
        {
            if (_currentCamera != null) return _currentCamera;
            if (_camera == null)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("场景中缺少照射的主摄像机，将照射相机Tag设置为MainCamera或给该类_camera属性赋值照射摄像机");
                    return null;
                }
                _currentCamera = Camera.main;
            }
            else
            {
                _currentCamera = _camera;
            }
            return _currentCamera;
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
