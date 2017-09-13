﻿using UnityEngine;
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
        [HideInInspector]
        public bool _isDrag = true;//是否可以拖拽
        [HideInInspector]
        public PointerEventData _pointerEventData;//事件数据
        [HideInInspector]
        public Vector3 _offset;//偏移点
        public Action<GameObject> _onDownBefore;//按下前委托动作
        public Action<GameObject> _onDown;//按下委托动作
        public Action<GameObject> _onBeginDrag;//开始拖拽委托动作
        public Action<GameObject> _onDrag;//拖拽中委托动作
        public Action<GameObject> _onEndDrag;//结束拖拽委托动作
        private Vector3 _screenSpace;//屏幕坐标
        private bool _draging = false;//是否拖拽中
        private bool _isDown = false;//是否按下
        public void OnPointerDown(PointerEventData evenData)
        {
            if (_isDown || _isDrag == false) return;
            if (_onDownBefore != null) _onDownBefore(gameObject);
            if (_camera == null)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("场景中缺少照射的主摄像机，将照射相机Tag设置为MainCamera或给该类_camera属性赋值照射摄像机");
                    return;
                }
                _screenSpace = Camera.main.WorldToScreenPoint(this.transform.position);
                _offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(evenData.position.x, evenData.position.y, _screenSpace.z));
            }
            else
            {
                _screenSpace = _camera.WorldToScreenPoint(this.transform.position);
                _offset = this.transform.position - _camera.ScreenToWorldPoint(new Vector3(evenData.position.x, evenData.position.y, _screenSpace.z));
            }
            _isDown = true;
            _pointerEventData = evenData;
            if (_onDown != null) _onDown(gameObject);
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
            Vector3 curScreenSpace = new Vector3(evenData.position.x, evenData.position.y, _screenSpace.z);
            if (_camera == null)
            {
                Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + _offset;
                this.transform.position = CurPosition;
            }
            else
            {
                Vector3 CurPosition = _camera.ScreenToWorldPoint(curScreenSpace) + _offset;
                this.transform.position = CurPosition;
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
    }
}
