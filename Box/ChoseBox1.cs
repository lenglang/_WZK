using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WZK
{
    /// <summary>
    /// 选择框1
    /// </summary>
    public class ChoseBox1 : MonoBehaviour
    {
        private enum MoveType
        {
            Null,
            上一页,
            下一页
        }
        [Header("上一页按钮")]
        public GameObject _prevButton;
        [Header("下一页按钮")]
        public GameObject _nextButton;
        /// <summary>
        /// 是否移动
        /// </summary>
        private bool _isMoving = false;
        private void Awake()
        {
            DownEvent.Get(_prevButton)._onDown = DownPrevButton;
            DownEvent.Get(_nextButton)._onDown = DownNextButton;
        }
        /// <summary>
        /// 前一页
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        private void DownPrevButton(PointerEventData evenData, GameObject obj, DownEvent etl)
        {
            DownButton(MoveType.上一页,obj.transform, etl);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="evenData"></param>
        /// <param name="obj"></param>
        /// <param name="etl"></param>
        private void DownNextButton(PointerEventData evenData, GameObject obj, DownEvent etl)
        {
            DownButton(MoveType.下一页,obj.transform, etl);
        }
        private void DownButton(MoveType mt,Transform tf,DownEvent etl)
        {
            if (_isMoving) return;
            _isMoving = true;
            //刷新按钮
            ButtonEffect.Scale(tf, delegate { etl.enabled = true; Moving(); });
        }
        /// <summary>
        /// 移动
        /// </summary>
        private void Moving()
        { }
    }
}
