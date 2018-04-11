using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace WZK
{
    /// <summary>
    /// 选择框1
    /// </summary>
    public class ChoseBox1 : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [Header("画布")]
        public Canvas _canvas;
        [Header("当前页面")]
        public Page _currentPage;
        [Header("下一个页面")]
        public Page _nextPage;
        [Header("上一页按钮")]
        public GameObject _prevButton;
        [Header("下一页按钮")]
        public GameObject _nextButton;
        [Header("每页Item数量")]
        public float _pageItemNum = 8;
        [Header("移动距离")]
        public float _moveDis = 800;
        [Header("总的Item数")]
        public float _totalItemNum = 0;
        [Header("滑动距离")]
        public float _slideDis = 100;
        /// <summary>
        /// 移动状态
        /// </summary>
        private enum MoveState
        {
            Null,
            上一页,
            下一页
        }
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
        /// 当前页索引
        /// </summary>
        private int _currentPageIndex = 1;
        /// <summary>
        /// 总的页面数量
        /// </summary>
        private int _totalPageNum = 0;
        private void Awake()
        {
            DownUpEvent.Get(_prevButton)._downAction = DownPrevButton;
            DownUpEvent.Get(_nextButton)._downAction = DownNextButton;
            _currentPage.Init(this);
            _nextPage.Init(this);
            if (_totalItemNum != 0) Init(_totalItemNum);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(float num)
        {
            _totalItemNum = num;
            _totalPageNum = (int)Mathf.Ceil(_totalItemNum / _pageItemNum);
            UpdateButton();
            _currentPage.RefreshData();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragOrigin = GetUIPosition(eventData.position);
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (_isMoving) return;
            Vector2 offset = GetUIPosition(eventData.position) - _dragOrigin;
            if (offset.x > 0 && offset.x > _slideDis&& _currentPageIndex > 1)
            {
                _moveState = MoveState.上一页;
                Moving();
            }
            else if (offset.x < 0 && Math.Abs(offset.x) > _slideDis && _currentPageIndex < _totalPageNum)
            {
                _moveState = MoveState.下一页;
                Moving();
            }
        }
        /// <summary>
        /// 前一页
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        private void DownPrevButton(PointerEventData evenData, GameObject obj, DownUpEvent etl)
        {
            DownButton(MoveState.上一页, obj.transform, etl);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        private void DownNextButton(PointerEventData evenData, GameObject obj, DownUpEvent etl)
        {
            DownButton(MoveState.下一页, obj.transform, etl);
        }
        /// <summary>
        /// 按下按钮
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="tf"></param>
        /// <param name="etl"></param>
        private void DownButton(MoveState ms, Transform tf, DownUpEvent etl)
        {
            if (_isMoving) return;
            _isMoving = true;
            _moveState = ms;
            ButtonEffect.Scale(tf, delegate { etl.enabled = true; Moving(); });
        }
        /// <summary>
        /// 移动
        /// </summary>
        private void Moving()
        {
            _isMoving = true;
            int dir = 1;//向右移
            if (_moveState == MoveState.下一页){ dir = -1; }
            _currentPageIndex -= dir;
            UpdateButton();
            _nextPage.RefreshData();
            _nextPage.transform.SetLocalX(_currentPage.transform.localPosition.x - dir * _moveDis);
            _nextPage.transform.DOLocalMoveX(_nextPage.transform.localPosition.x + dir * _moveDis, 0.3f);
            _currentPage.transform.DOLocalMoveX(_currentPage.transform.localPosition.x + dir * _moveDis, 0.3f).OnComplete(delegate 
            {
                Page temp = _currentPage;
                _currentPage = _nextPage;
                _nextPage = temp;
                _isMoving = false;
            });
        }
        /// <summary>
        /// 更新按钮状态
        /// </summary>
        private void UpdateButton()
        {
            if (_currentPageIndex == 1) { _prevButton.SetActive(false); }
            else { _prevButton.SetActive(true); }
            if (_currentPageIndex == _totalPageNum) { _nextButton.SetActive(false); }
            else { _nextButton.SetActive(true); }
        }
        /// <summary>
        /// 获取UI坐标
        /// </summary>
        private Vector2 GetUIPosition(Vector2 pos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,pos, _canvas.worldCamera, out pos);
            return pos;
        }
    }
}
