using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace WZK
{
    /// <summary>
    /// 作者-wzk
    /// 功能-3D物体拖拽
    /// 使用说明-直接以组件形式添加到物体上，通过设置_isDrag的bool值来开启和禁用3D物体拖拽功能，设置_camera来指定照射相机
    /// 注意事项-场景需添加EventSystem事件系统，照射相机需添加物理射线，3D物体需有Collider相关组件
    /// </summary>
    [AddComponentMenu("Common/Gestures/DragGestures3D")]
    public class DragGestures3D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector]
        public Camera _camera;//照射相机
        private Camera _currentCamera;//当前相机
        [HideInInspector]
        public bool _isDrag = true;//是否可以拖拽
        [HideInInspector]
        public PointerEventData _pointerEventData;//事件数据
        [HideInInspector]
        public Vector3 _offset;//偏移点
        [HideInInspector]
        public Transform _moveOutJudgePoint;//移动出判断点
        [HideInInspector]
        public bool _isDown = false;//是否按下
        [HideInInspector]
        public Rect _rectEdge = new Rect(0, 0, 0, 0);//边缘
        public Action<GameObject> _onDownBefore;//按下前委托动作
        public Action<GameObject> _onDown;//按下委托动作
        public Action<GameObject> _onBeginDrag;//开始拖拽委托动作
        public Action<GameObject> _onDrag;//拖拽中委托动作
        public Action<GameObject> _onEndDrag;//结束拖拽委托动作
        public Action<GameObject> _onDownAny;//按下不受影响
        public Action<GameObject> _onUpAny;//弹起不受影响
        private Vector3 _lastPosition;//上一个位置
        private Vector3 _screenSpace;//屏幕坐标
        private bool _draging = false;//是否拖拽中
        private void Awake()
        {
            _moveOutJudgePoint = transform.FindChild("移出判断点");
        }
        /// <summary>
        /// 状态初始
        /// </summary>
        public void InIt()
        {
            _isDown = false;
            _draging = false;
        }
        public void OnPointerDown(PointerEventData evenData)
        {
            _pointerEventData = evenData;
            if (_onDownAny != null) _onDownAny(gameObject);
            if (_isDown || _isDrag == false) return;
            if (_onDownBefore != null) _onDownBefore(gameObject);
            UpdateOffset(evenData);
            _isDown = true;
            if (_onDown != null) _onDown(gameObject);
        }
        /// <summary>
        /// 对焦到鼠标点
        /// </summary>
        public void ResetToMouse(Vector3 offset = default(Vector3))
        {
            _offset = offset;
            _screenSpace = GetCamera().WorldToScreenPoint(transform.position);
            transform.position = GetCamera().ScreenToWorldPoint(new Vector3(_pointerEventData.position.x, _pointerEventData.position.y, _screenSpace.z)) + _offset;
        }
        /// <summary>
        /// 更新差值
        /// </summary>
        public void UpdateOffset(PointerEventData evenData)
        {
            _screenSpace = GetCamera().WorldToScreenPoint(transform.position);
            _offset = transform.position - GetCamera().ScreenToWorldPoint(new Vector3(evenData.position.x, evenData.position.y, _screenSpace.z));
        }
        public void OnBeginDrag(PointerEventData evenData)
        {
            if (_isDown == false || _isDrag == false) return;
            _draging = true;
            UpdatePosition(evenData);
            if (_onBeginDrag != null) _onBeginDrag(gameObject);
        }
        public void OnDrag(PointerEventData evenData)
        {
            if (_isDown == false || _isDrag == false) return;
            UpdatePosition(evenData);
            if (_onDrag != null) _onDrag(gameObject);
        }
        public void UpdatePosition(PointerEventData evenData)
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
        public void OnEndDrag(PointerEventData evenData)
        {
            //有拖动才会执行
            if (_draging == false || _isDrag == false) return;
            _draging = false;
            _pointerEventData = evenData;
            if (_onEndDrag != null) _onEndDrag(gameObject);
        }
        public void OnPointerUp(PointerEventData evenData)
        {
            if (_onUpAny != null) _onUpAny(gameObject);
            if (_isDown == false || _isDrag == false) return;
            _isDown = false;
            _pointerEventData = evenData;
            if (_draging == false)
            {
                if (_onEndDrag != null) _onEndDrag(gameObject);
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
                if (_onEndDrag != null && _isDown && _isDrag) _onEndDrag(gameObject);
            }
        }
        /// <summary>
        /// 获取相机
        /// </summary>
        /// <returns></returns>
        public Camera GetCamera()
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
    }
}
