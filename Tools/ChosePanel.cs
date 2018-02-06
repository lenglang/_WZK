using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
namespace WZK
{
    /// <summary>
    /// 选择面板
    /// </summary>
    public class ChosePanel : WZK.Singleton<ChosePanel>
    {
        //public enum EventType
        //{
        //   更新,
        //   替换完成
        //}
        //[Header("面板")]
        //public GameObject _panel;
        //[Header("容器")]
        //public RectTransform _container;//容器
        //[Header("上箭头")]
        //public GameObject _upArrow;
        //[Header("下箭头")]
        //public GameObject _downArrow;
        //[Header("3D相机")]
        //public Camera _3DCamera;
        //[Header("完成按钮")]
        //public GameObject _completeButton;
        //[Header("Item预制体")]
        //public GameObject _item;
        //public Action _downAction;//按下回调
        //private int _totalPage = 0;//总页数
        //private int _currentPage = 1;
        //private SlideGestures _slideGestures;//滑动
        //private bool _isMoveing = false;//是否正在移动
        //private MeshRenderer _currentMeshRenderer;//当前meshRenderer
        //public DragGestures3D _dragGestures;//拖拽手势
        //private Vector2 _downPosition;//按下点
        //private Vector2 _currentPosition;//当前点
        //private List<DragGestures3D> _dragGesturesList = new List<DragGestures3D>();//手势列表
        //[HideInInspector]
        //public GameObject _inductionArea;//感应区
        //private List<GameObject> _dragObjectList = new List<GameObject>();//拖拽物体列表
        //private string _direction = "";
        //private List<GameObject> _itemList = new List<GameObject>();//item列表
        //private DragGestures3D _currentItemDragGesture3D;//当前物体
        public void Start()
        {
            //_panel.SetActive(false);
            
        }
        /// <summary>
        /// 初始化
        /// </summary>
        //public void InIt(List<GameObject> dragObjectList, GameObject inductionArea,Action downAction=null,bool voice=true)
        //{
        //    if (MachineManager.Instance.isFirstDecoration&& voice)
        //    {
        //        SoundManager.Instance.PlayVoice(VoiceType.我们给玩具装饰一下吧);
        //        MachineManager.Instance.isFirstDecoration = false;
        //    }
        //    _downAction = downAction;
        //    _upArrow.SetActive(false);
        //    _downArrow.SetActive(false);
        //    _currentPage = 1;
        //    _itemList.Clear();
        //    if (_container.childCount != 0)
        //    {
        //        _container.DestroyChildren();
        //    }
        //    _dragObjectList.Clear();
        //    _dragObjectList.AddRange(dragObjectList);
        //    _inductionArea = inductionArea;
        //    Vector3 lp = _item.transform.localPosition;
        //    GameObject itemObj;
        //    //随机排序
        //    List<GameObject> tempList = new List<GameObject>();
        //    tempList.AddRange(dragObjectList);
        //    dragObjectList.Clear();
        //    int r = 0;
        //    do
        //    {
        //        r = UnityEngine.Random.Range(0, tempList.Count);
        //        dragObjectList.Add(tempList[r]);
        //        tempList.RemoveAt(r);
        //    }
        //    while (tempList.Count>0);
        //    for (int i = 0; i < dragObjectList.Count; i++)
        //    {
        //        itemObj = Instantiate(_item);
        //        itemObj.transform.parent = _container;
        //        itemObj.transform.localPosition = lp;
        //        itemObj.transform.Translate(0, -170 * i, 0);
        //        dragObjectList[i].transform.parent = itemObj.transform;
        //        dragObjectList[i].GetComponent<GameObjectPosition>().SetInformation("初始");
        //        itemObj.name = "Item" + i;
        //        _itemList.Add(itemObj);
        //    }
        //    _totalPage = dragObjectList.Count - 3 + 1;
        //    SoundManager.Instance.PlaySound(SoundType.右侧选择栏弹出);
        //    ShowHidePanel(_panel, delegate
        //    {
        //        this.InvokeWaitForSeconds(delegate
        //        {
        //            if (_totalPage > 1)
        //            {
        //                int num = _totalPage - 1;
        //                float targetY = (_totalPage - 1) * 170;
        //                _container.DOLocalMoveY(targetY, 0.3f * num).SetEase(Ease.Linear).OnComplete(delegate
        //                {
        //                    targetY = 0;
        //                    _container.DOLocalMoveY(targetY, 0.3f * num).SetEase(Ease.Linear).OnComplete(delegate
        //                    {
        //                        AddEvent();
        //                    });
        //                });
        //            }
        //            else
        //            {
        //                AddEvent();
        //            }
        //        }, 0.3f);
        //    });
        //}
        /// <summary>
        /// 添加事件
        /// </summary>
        //private void AddEvent()
        //{
        //    EventTriggerListener.Get(_upArrow)._onClick = DownMove;
        //    EventTriggerListener.Get(_downArrow)._onClick = UpMove;
        //    for (int i = 0; i < _dragObjectList.Count; i++)
        //    {
        //        AddDragGestures(_dragObjectList[i]);
        //    }
        //    UpdateArrow();
        //}
        ///// <summary>
        ///// 添加手势
        ///// </summary>
        ///// <param name="obj"></param>
        //private void AddDragGestures(GameObject obj)
        //{
        //    DragGestures3D dragGestures;
        //    dragGestures = obj.AddComponent<DragGestures3D>();
        //    dragGestures._onDown = DownItem;
        //    dragGestures._onDrag = DragItem;
        //    dragGestures._onEndDrag = EndDragItem;
        //    dragGestures._camera = _3DCamera;
        //    dragGestures._onDownBefore = DownBeforeStickers;
        //    _dragGesturesList.Add(dragGestures);
        //    _currentItemDragGesture3D = dragGestures;
        //}
        ///// <summary>
        ///// 按下前
        ///// </summary>
        ///// <param name="obj"></param>
        //private void DownBeforeStickers(GameObject obj)
        //{
        //    obj.transform.SetLocalZ(-200);
        //}
        ///// <summary>
        ///// 按下选项
        ///// </summary>
        ///// <param name="obj"></param>
        //private void DownItem(GameObject obj)
        //{
        //    if (_dragGestures != null) return;
        //    SoundManager.Instance.PlaySound(SoundType.通用点击);
        //    _dragGestures = obj.GetComponent<DragGestures3D>();
        //    _downPosition = _dragGestures._pointerEventData.position;
        //    GameObject newObj = Instantiate(obj);
        //    newObj.transform.SetParent(obj.transform.parent);
        //    newObj.transform.parent.localScale *= 1.1f;
        //    newObj.transform.localPosition = obj.transform.localPosition;
        //    obj.transform.SetParent(transform);
        //    newObj.name = obj.name;
        //    newObj.transform.SetLocalZ(-70);
        //    _currentMeshRenderer = obj.GetComponent<MeshRenderer>();
        //    _currentMeshRenderer.enabled = false;
        //    Destroy(newObj.GetComponent<DragGestures3D>());
        //    AddDragGestures(newObj);
        //    CancelOtherDragEvent();
        //    if (_downAction != null) _downAction();
        //}
        ///// <summary>
        ///// 拖拽
        ///// </summary>
        ///// <param name="obj"></param>
        //private void DragItem(GameObject obj)
        //{
        //    if (_currentMeshRenderer.enabled) return;
        //    _currentPosition = _dragGestures._pointerEventData.position;
        //    if (_currentPosition.y - _downPosition.y > Screen.height/10)
        //    {
        //        RemoveDragGestures();
        //        UpMove(null, null);
        //        if (_isMoveing == false) OpenDragEvent();
        //        return;
        //    }
        //    else if (_currentPosition.y - _downPosition.y < -Screen.height / 10)
        //    {
        //        RemoveDragGestures();
        //        DownMove(null, null);
        //        if (_isMoveing == false) OpenDragEvent();
        //        return;
        //    }
        //    if (_currentPosition.x - _downPosition.x < 0 && Mathf.Abs(_currentPosition.x - _downPosition.x) > 30)
        //    {
        //        if (_currentMeshRenderer != null)
        //        {
        //            _currentMeshRenderer.enabled = true;
        //            _currentMeshRenderer.gameObject.transform.localScale *= 2f;
        //            if (obj.name.Contains("数字") && RepairManager.Instance._repairObject.name == "闹钟")
        //            {
        //                VoiceType myEnum = (VoiceType)Enum.Parse(typeof(VoiceType), obj.name, true);
        //                SoundManager.Instance.PlayVoice(myEnum);
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// 结束拖拽
        ///// </summary>
        ///// <param name="obj"></param>
        //private void EndDragItem(GameObject obj)
        //{
        //    _dragGesturesList[_dragGesturesList.Count - 1].transform.parent.localScale = Vector3.one;
        //    Vector3 v = _3DCamera.WorldToScreenPoint(obj.transform.position); 
        //    if (_inductionArea.IsRayHit(v))
        //    {

        //        if (MachineManager.Instance._repairObject.name.Substring(0,2) != "闹钟")
        //        {
        //            NotificationManager<EventType, int>.Instance.DispatchEvent(EventType.更新, int.Parse(obj.name.Split('-')[1]));
        //            if(obj.name.Split('-')[0]!= "yinyuehe" )
        //            OpenCompleteButton();
        //        }
        //        else
        //        {
        //            NotificationManager<EventType, int>.Instance.DispatchEvent(EventType.更新, int.Parse(obj.name.Substring(2, 1)));
        //            return;
        //        }
        //    }
        //    RemoveDragGestures();
        //    OpenDragEvent();
        //}
        ///// <summary>
        ///// 开启完成按钮
        ///// </summary>
        //public void OpenCompleteButton()
        //{
        //    TipManager.Instance.SureButtonLoopTip(_completeButton);
        //    EventTriggerListener.Get(_completeButton)._onClick = ClickComplete;
        //}
        ///// <summary>
        ///// 点击完成
        ///// </summary>
        ///// <param name="evenData"></param>
        ///// <param name="obj"></param>
        //private void ClickComplete(PointerEventData evenData, GameObject obj)
        //{
        //    SoundManager.Instance.PlaySound(SoundType.通用点击);
        //    LoopActionManager<TipManager.TipType>.Instance.RemoveLoopAction(TipManager.TipType.喷漆打钩箭头提示);
        //    if (_dragGestures != null) Destroy(_dragGestures.gameObject);
        //    _dragGestures = null;
        //    CancelOtherDragEvent();
        //    Destroy(obj.GetComponent<EventTriggerListener>());
        //    ButtonEffect.BigSmall(_completeButton,delegate
        //    {
        //        //Destroy(_completeButton);
        //        _completeButton.SetActive(false);
        //        ShowHidePanel(_panel, delegate
        //        {
        //            NotificationManager<EventType>.Instance.DispatchEvent(EventType.替换完成);
        //        },false);
        //    });

        //} 
        ///// <summary>
        ///// 取消拖拽
        ///// </summary>
        //public void CancelOtherDragEvent(bool all=false)
        //{
        //    for (int i = 0; i < _dragGesturesList.Count; i++)
        //    {
        //        if (_dragGesturesList[i] != _dragGestures)_dragGesturesList[i]._isDrag = false;
        //    }
        //}
        ///// <summary>
        ///// 开启拖拽
        ///// </summary>
        //public void OpenDragEvent()
        //{
        //    for (int i = 0; i < _dragGesturesList.Count; i++)
        //    {
        //        _dragGesturesList[i]._isDrag = true;
        //    }
        //}
        //public void ResetPosition()
        //{
        //    for (int i = 0; i < _dragGesturesList.Count; i++)
        //    {
        //        if (_currentItemDragGesture3D == _dragGesturesList[i])
        //        {
        //            _dragGesturesList.RemoveAt(i);
        //            break;
        //        }
        //    }
        //    _itemList.Clear();
        //    _dragGesturesList.Add(_currentItemDragGesture3D);
        //    Vector3 lp = _item.transform.localPosition;
        //    for (int i = 0; i < _dragGesturesList.Count; i++)
        //    {
        //        _dragGesturesList[i].transform.parent.transform.localPosition = lp;
        //        _dragGesturesList[i].transform.parent.transform.Translate(0, -170 * i, 0);
        //        _itemList.Add(_dragGesturesList[i].transform.parent.gameObject);
        //    }
        //}
        ///// <summary>
        ///// 移除手势
        ///// </summary>
        //public void RemoveDragGestures(bool isDestory = true)
        //{
        //    for (int i = 0; i < _dragGesturesList.Count; i++)
        //    {
        //        if (_dragGesturesList[i] == _dragGestures) _dragGesturesList.RemoveAt(i);
        //    }
        //    if (isDestory) 
        //    Destroy(_dragGestures.gameObject);
        //    _dragGestures = null;
        //    _currentMeshRenderer = null;
        //}
        ///// <summary>
        ///// 上移
        ///// </summary>
        ///// <param name="evenData"></param>
        ///// <param name="obj"></param>
        //private void UpMove(PointerEventData evenData, GameObject obj)
        //{

        //    if (_isMoveing) return;
        //    _direction = "上移";
        //    _isMoveing = true;
        //    if (obj != null)
        //    {
        //        SoundManager.Instance.PlaySound(SoundType.通用点击);
        //        ButtonEffect.BigSmall(obj, Moveing, 1.5f);
        //    }
        //    else
        //    {
        //        Moveing();
        //    }
        //}
        ///// <summary>
        ///// 下移
        ///// </summary>
        ///// <param name="evenData"></param>
        ///// <param name="obj"></param>
        //private void DownMove(PointerEventData evenData, GameObject obj)
        //{
        //    if (_isMoveing) return;
        //    _direction = "下移";
        //    _isMoveing = true;
        //    if (obj != null)
        //    {
        //        SoundManager.Instance.PlaySound(SoundType.通用点击);
        //        ButtonEffect.BigSmall(obj, Moveing, 1.5f);
        //    }
        //    else
        //    {
        //        Moveing();
        //    }
        //}
        ///// <summary>
        ///// 移动
        ///// </summary>
        //private void Moveing()
        //{
        //    _dragGesturesList[_dragGesturesList.Count - 1].transform.parent.localScale = Vector3.one;
        //    _isMoveing = true;
        //    float targetY = 0;
        //    List<GameObject> tempList = new List<GameObject>();
        //    Vector2 p;
        //    if (_direction == "下移")
        //    {
        //        for (int i = _itemList.Count - 1; i >= 0; i--)
        //        {
        //            if (_itemList[i].transform.localPosition.y <= 156 - 3 * 170)
        //            {
        //                tempList.Add(_itemList[i]);
        //                _itemList.RemoveAt(i);
        //            }
        //        }
        //        for (int i = 0; i < tempList.Count; i++)
        //        {
        //            p = tempList[i].transform.localPosition;
        //            p.y = _itemList[0].transform.localPosition.y + 170;
        //            tempList[i].transform.localPosition = p;
        //            _itemList.Insert(0, tempList[i]);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = _itemList.Count - 1; i >= 0; i--)
        //        {
        //            if (_itemList[i].transform.localPosition.y >= 156 + 170)
        //            {
        //                tempList.Add(_itemList[i]);
        //                _itemList.RemoveAt(i);
        //            }
        //        }

        //        for (int i = tempList.Count - 1; i >= 0; i--)
        //        {
        //            p = tempList[i].transform.localPosition;
        //            p.y = _itemList[_itemList.Count - 1].transform.localPosition.y - 170;
        //            tempList[i].transform.localPosition = p;
        //            _itemList.Add(tempList[i]);
        //        }
        //    }
        //    for (int i = 0; i < _itemList.Count; i++)
        //    {
        //        if (_direction == "下移") { targetY = _itemList[i].transform.localPosition.y - 170; }
        //        else { targetY = _itemList[i].transform.localPosition.y + 170; }
        //        _itemList[i].transform.DOLocalMoveY(targetY, 0.3f).SetEase(Ease.Linear);
        //    }
        //    tempList.Clear();
        //    this.InvokeWaitForSeconds(delegate
        //    {
        //        _isMoveing = false;
        //        OpenDragEvent();
        //    }, 0.31f);
        //}
        ///// <summary>
        ///// 更新方向键
        ///// </summary>
        //private void UpdateArrow()
        //{
        //    _upArrow.SetActive(true);
        //    _downArrow.SetActive(true);
        //}
        ///// <summary>
        ///// 显示隐藏
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="complete"></param>
        ///// <param name="ToIn"></param>
        //public void ShowHidePanel(GameObject obj, Action complete = null, bool ToIn = true)
        //{
        //    float w = obj.GetComponent<RectTransform>().rect.width;
        //    // obj.GetComponent<RectTransform>().localPosition.x = -112;
        //    Vector3 oldPos = obj.transform.localPosition;
        //    Vector3 target = obj.transform.localPosition;
        //    Vector3 outPosition = target;
        //    outPosition.x += w;
        //    if (ToIn)
        //    {
        //        obj.SetActive(true);
        //        obj.transform.localPosition = outPosition;
        //    }
        //    else
        //    {
        //        target = outPosition;
        //    }
        //    obj.transform.DOLocalMoveX(target.x, 0.3f).OnComplete(delegate
        //    {
        //        if (ToIn == false)
        //        {
        //            obj.transform.localPosition = oldPos;
        //            obj.SetActive(false);
        //        }
        //        if (complete != null) complete();
        //    }).SetEase(Ease.Linear);
        //}
    }
}
