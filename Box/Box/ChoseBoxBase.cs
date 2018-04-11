//=======================================================
// 作者：王则昆
// 描述：选择框基类
//=======================================================
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;
using System;
namespace WZK
{
    public class ChoseBoxBase : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerExitHandler, IEndDragHandler
    {
        /// <summary>
        /// 移动状态
        /// </summary>
        private enum MoveState
        {
            Null,
            上一个,
            下一个
        }
        [Header("前一个按钮")]
        public GameObject _prevButton;
        [Header("下一个按钮")]
        public GameObject _nextButton;
        [Header("画布")]
        public Canvas _canvas;
        [Header("移动距离")]
        public float _moveDis = 200;
        [Header("滑动距离")]
        public float _slideDis = 100;
        [Header("是否水平移动")]
        public bool _isHorizontal = false;
        [Header("item容器")]
        public RectTransform _container;
        [Header("item预设")]
        public GameObject _item;
        [Header("面板")]
        public RectTransform _panel;
        [Header("面板移动距离")]
        public float _PanelMoveDis = 300;
        [Header("展示个数")]
        public int _num = 3;
        [Header("是否循环展示")]
        public bool _isLoop = false;
        /// <summary>
        /// 碰撞检测发射线相机
        /// </summary>
        [HideInInspector]
        public Camera _camera;
        /// <summary>
        /// 碰撞检测物体
        /// </summary>
        [HideInInspector]
        public GameObject _hit;
        /// <summary>
        /// 物体索引
        /// </summary>
        [HideInInspector]
        public int _layerIndex = 0;
        /// <summary>
        /// 手指ID
        /// </summary>
        [HideInInspector]
        private int _pointerId = 100;
        /// <summary>
        /// 拖拽物体
        /// </summary>
        [HideInInspector]
        public GameObject _dragObject;
        /// <summary>
        /// 按下状态
        /// </summary>
        public Action<GameObject> _downStateAction;
        /// <summary>
        /// 状态还原
        /// </summary>
        public Action<GameObject> _upsStateAction;
        /// <summary>
        /// 创建拖拽物
        /// </summary>
        public Action<GameObject> _createDragObjectAction;
        /// <summary>
        /// 放置成功
        /// </summary>
        public Action<string> _putSuccess;
        /// <summary>
        /// 是否移动
        /// </summary>
        private bool _isMoving = false;
        /// <summary>
        /// 拖拽起始点
        /// </summary>
        private Vector2 _dragOrigin;
        /// <summary>
        /// 移动状态
        /// </summary>
        private MoveState _moveState;
        /// <summary>
        /// item列表
        /// </summary>
        [HideInInspector]
        public List<GameObject> _itemList = new List<GameObject>();
        /// <summary>
        /// 是否被拖
        /// </summary>
        private bool _isDraging = false;
        /// <summary>
        /// 选择Item
        /// </summary>
        private GameObject _chosedItem;
        /// <summary>
        /// 是否可移动
        /// </summary>
        private bool _canMoving = false;
        /// <summary>
        /// 当前位置
        /// </summary>
        private int _currentPosition = 0;
        /// <summary>
        /// 最大位置
        /// </summary>
        private int _maxPosition = 0;
        /// <summary>
        /// 最小位置
        /// </summary>
        private int _minPosition = 0;
        /// <summary>
        /// 是否滚动展示
        /// </summary>
        /// <param name="isShow"></param>
        public void Init(bool isShow = true)
        {
            int count = _itemList.Count - _num;
            _maxPosition = count;
            if (count > 0 && isShow)
            {
                PanelMove(delegate
                {
                    Vector3 origin = _container.localPosition;
                    Vector3 target;
                    if (_isHorizontal)
                    {
                        target = new Vector3(-count * _moveDis + origin.x, origin.y, 0);
                    }
                    else
                    {
                        target = new Vector3(origin.x, count * _moveDis + origin.y, 0);
                    }
                    _container.DOLocalMove(target, count * 0.3f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(delegate
                    {
                        AddEvent();
                    });
                });
            }
            else
            {
                AddEvent();
            }
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            _canMoving = true;
            DownUpEvent.Get(_prevButton)._downAction = DownPrevButton;
            DownUpEvent.Get(_nextButton)._downAction = DownNextButton;
            for (int i = 0; i < _itemList.Count; i++)
            {
                DownUpEvent.Get(_itemList[i])._downAction = DownItem;
                DownUpEvent.Get(_itemList[i])._upAction = UpItem;
            }
            UpdateButton();
        }
        /// <summary>
        /// 更新按钮
        /// </summary>
        private void UpdateButton()
        {
            if (_itemList.Count > 3)
            {
                _prevButton.SetActive(true);
                _nextButton.SetActive(true);
                if (_isLoop == false)
                {
                    _prevButton.SetActive(_currentPosition != _minPosition);
                    _nextButton.SetActive(_currentPosition != _maxPosition);
                }
            }
        }
        /// <summary>
        /// 面板移动
        /// </summary>
        /// <param name="isIn"></param>
        /// <param name="complete"></param>
        private void PanelMove(Action complete = null, bool isIn = true)
        {
            Vector2 target = Vector2.zero;
            if (_isHorizontal == false)
            {
                target.x = _PanelMoveDis;
                if (isIn) target.x *= -1;
                target.x += _panel.transform.localPosition.x;
            }
            else
            {
                target.y = -_PanelMoveDis;
                if (isIn) target.y *= -1;
                target.y += _panel.transform.localPosition.y;
            }
            _panel.DOLocalMove(target, 0.5f).OnComplete(delegate
            {
                if (complete != null) complete();
            });
        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        public void HidePanel()
        {
            _canMoving = false;
            if (_dragObject != null)
            {
                Destroy(_dragObject);
                _dragObject = null;
            }
            ItemStateBack();
            PanelMove(delegate
            {
                for (int i = 0; i < _itemList.Count; i++)
                {
                    Destroy(_itemList[i]);
                    _itemList[i] = null;
                }
                _itemList.Clear();
                _prevButton.SetActive(false);
                _nextButton.SetActive(false);
            }, false);
            _pointerId = 100;
        }
        /// <summary>
        /// 按下Item
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        public void DownItem(PointerEventData eventData, GameObject obj, DownUpEvent etl)
        {
            if (_canMoving == false || _pointerId != 100) return;
            _pointerId = eventData.pointerId;
            _chosedItem = obj;
            if (_downStateAction != null) _downStateAction(_chosedItem);
        }
        /// <summary>
        /// 放开Item
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        public void UpItem(PointerEventData evenData, GameObject obj, DownUpEvent etl)
        {
            if (evenData.pointerId != _pointerId || _isDraging || _canMoving == false) return;
            _pointerId = 100;
            ItemStateBack();
        }
        /// <summary>
        /// 离开
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerId == _pointerId && _canMoving)
            {
                _isDraging = true;
                _canMoving = false;
                if (_chosedItem != null) _createDragObjectAction(_chosedItem);
                ItemStateBack();
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.pointerId == _pointerId && _canMoving)
            {
                _isDraging = true;
                _dragOrigin = GetUIPosition(eventData.position);
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (_dragObject != null)
            {
                _dragObject.transform.position = GetUIPosition(eventData.position);
            }
            if (_isMoving || _canMoving == false || eventData.pointerId != _pointerId) return;
            Vector2 offset = GetUIPosition(eventData.position) - _dragOrigin;
            if ((_isHorizontal && offset.x > 0 && offset.x > _slideDis) || (_isHorizontal == false && offset.y < 0 && Mathf.Abs(offset.y) > _slideDis))
            {
                _moveState = MoveState.上一个;
                if (_isLoop || (_isLoop == false && _currentPosition > _minPosition)) Moving();
            }
            else if ((_isHorizontal && offset.x < 0 && Mathf.Abs(offset.x) > _slideDis) || (_isHorizontal == false && offset.y > 0 && offset.y > _slideDis))
            {
                _moveState = MoveState.下一个;
                if (_isLoop || (_isLoop == false && _currentPosition < _maxPosition)) Moving();
            }
        }
        /// <summary>
        /// 结束拖拽
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerId == _pointerId)
            {
                _pointerId = 100;
                _isDraging = false;
                _canMoving = true;
                ItemStateBack();
                if (_dragObject != null)
                {
                    if (_hit.IsRayHit(eventData.position, _camera, _layerIndex)) _putSuccess(_dragObject.name);
                    Destroy(_dragObject);
                    _dragObject = null;
                }
            }
        }
        /// <summary>
        /// item状态还原
        /// </summary>
        private void ItemStateBack()
        {
            if (_chosedItem != null)
            {
                if (_upsStateAction != null) _upsStateAction(_chosedItem);
                _chosedItem = null;
            }
        }
        /// <summary>
        /// 前一个
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        private void DownPrevButton(PointerEventData evenData, GameObject obj, DownUpEvent etl)
        {
            DownButton(MoveState.上一个, obj.transform, etl);
        }
        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        private void DownNextButton(PointerEventData evenData, GameObject obj, DownUpEvent etl)
        {
            DownButton(MoveState.下一个, obj.transform, etl);
        }
        /// <summary>
        /// 按下按钮
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="tf"></param>
        /// <param name="etl"></param>
        private void DownButton(MoveState ms, Transform tf, DownUpEvent etl)
        {
            if (_isMoving || _canMoving == false) return;
            _isMoving = true;
            _moveState = ms;
            ButtonEffect.Scale(tf, Moving, 1.4f);
        }
        /// <summary>
        /// 移动
        /// </summary>
        private void Moving()
        {
            _isMoving = true;
            Vector2 target = Vector2.zero;
            List<GameObject> tempList = new List<GameObject>();
            Vector2 p;
            if (_isLoop)
            {
                if (_moveState == MoveState.上一个)
                {
                    if (_isHorizontal)
                    {
                        for (int i = _itemList.Count - 1; i >= 0; i--)
                        {
                            if (_itemList[i].transform.localPosition.x >= (_num - 1) * _moveDis)
                            {
                                tempList.Add(_itemList[i]);
                                _itemList.RemoveAt(i);
                            }
                        }
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            p = tempList[i].transform.localPosition;
                            p.x = _itemList[0].transform.localPosition.x - _moveDis;
                            tempList[i].transform.localPosition = p;
                            _itemList.Insert(0, tempList[i]);
                        }
                    }
                    else
                    {
                        for (int i = _itemList.Count - 1; i >= 0; i--)
                        {
                            if (_itemList[i].transform.localPosition.y <= -(_num - 1) * _moveDis)
                            {
                                tempList.Add(_itemList[i]);
                                _itemList.RemoveAt(i);
                            }
                        }
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            p = tempList[i].transform.localPosition;
                            p.y = _itemList[0].transform.localPosition.y + _moveDis;
                            tempList[i].transform.localPosition = p;
                            _itemList.Insert(0, tempList[i]);
                        }
                    }
                }
                else
                {
                    if (_isHorizontal)
                    {
                        for (int i = _itemList.Count - 1; i >= 0; i--)
                        {
                            if (_itemList[i].transform.localPosition.x <= -(_num - 1) * _moveDis)
                            {
                                tempList.Add(_itemList[i]);
                                _itemList.RemoveAt(i);
                            }
                        }

                        for (int i = tempList.Count - 1; i >= 0; i--)
                        {
                            p = tempList[i].transform.localPosition;
                            p.x = _itemList[_itemList.Count - 1].transform.localPosition.x + _moveDis;
                            tempList[i].transform.localPosition = p;
                            _itemList.Add(tempList[i]);
                        }
                    }
                    else
                    {
                        for (int i = _itemList.Count - 1; i >= 0; i--)
                        {
                            if (_itemList[i].transform.localPosition.y >= (_num - 1) * _moveDis)
                            {
                                tempList.Add(_itemList[i]);
                                _itemList.RemoveAt(i);
                            }
                        }

                        for (int i = tempList.Count - 1; i >= 0; i--)
                        {
                            p = tempList[i].transform.localPosition;
                            p.y = _itemList[_itemList.Count - 1].transform.localPosition.y - _moveDis;
                            tempList[i].transform.localPosition = p;
                            _itemList.Add(tempList[i]);
                        }
                    }

                }
            }
            else
            {
                if (_moveState == MoveState.上一个)
                {
                    _currentPosition--;
                }
                else
                {
                    _currentPosition++;
                }
                UpdateButton();
            }
            for (int i = 0; i < _itemList.Count; i++)
            {
                if (_moveState == MoveState.上一个)
                {
                    if (_isHorizontal)
                    {
                        target.x = _itemList[i].transform.localPosition.x + _moveDis;
                    }
                    else
                    {
                        target.y = _itemList[i].transform.localPosition.y - _moveDis;
                    }
                }
                else
                {
                    if (_isHorizontal)
                    {
                        target.x = _itemList[i].transform.localPosition.x - _moveDis;
                    }
                    else
                    {
                        target.y = _itemList[i].transform.localPosition.y + _moveDis;
                    }
                }
                _itemList[i].transform.DOLocalMove(target, 0.3f).SetEase(Ease.Linear);
            }
            tempList.Clear();
            this.InvokeWaitForSeconds(delegate
            {
                _isMoving = false;
            }, 0.31f);
        }
        /// <summary>
        /// 获取UI坐标
        /// </summary>
        private Vector2 GetUIPosition(Vector2 pos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, pos, _canvas.worldCamera, out pos);
            return pos;
        }
    }
}
