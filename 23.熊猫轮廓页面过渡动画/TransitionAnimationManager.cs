using System;
using UnityEngine;
namespace WZK
{
    public class TransitionAnimationManager : MonoBehaviour
    {
        private static TransitionAnimationManager _instance;
        public static TransitionAnimationManager Instance
        {
            get
            {
                return _instance;
            }
        }
        [Header("面板")]
        public GameObject _plane;
        private Material _material;
        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            _material = _plane.GetComponent<MeshRenderer>().material;
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            _instance = null;
        }
        #region 变小
        private float _sTiling = 50f;
        private float _sOffset = -24.5f;
        #endregion
        #region 变大
        private float _bTiling = 1f;
        private float _bOffset = 0;
        #endregion
        private float _cTiling = 0;
        private float _cOffset = 0;
        private Action _action1;
        private Action _action2;
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="action1">熊猫轮廓变到最小时执行的回调</param>
        /// <param name="action2">动画结束时执行的回调</param>
        public void PlayAnimation(Action action1, Action action2 = null)
        {
            gameObject.SetActive(true);
            _action1 = action1;
            _action2 = action2;
            _cTiling = _bTiling;
            _cOffset = _bOffset;
            //标记
            //DOTween.To(() => _cTiling, x => _cTiling = x, _sTiling, 0.8f).OnUpdate(() => UpdateTiling(_cTiling)).SetEase(Ease.OutQuad);
            //DOTween.To(() => _cOffset, x => _cOffset = x, _sOffset, 0.8f).OnUpdate(() => UpdateOffset(_cOffset)).SetEase(Ease.OutQuad).OnComplete(ToBig);
        }
        private void ToBig()
        {
            if (_action1 != null) _action1();
            //标记
            //DOTween.To(() => _cTiling, x => _cTiling = x, _bTiling, 1f).OnUpdate(() => UpdateTiling(_cTiling)).SetEase(Ease.InQuad);
            //DOTween.To(() => _cOffset, x => _cOffset = x, _bOffset, 1f).OnUpdate(() => UpdateOffset(_cOffset)).SetEase(Ease.InQuad).OnComplete(delegate { if (_action2 != null) _action2(); gameObject.SetActive(false); });
        }
        private void UpdateTiling(float tiling)
        {
            _material.SetTextureScale("_MainTex", new Vector2(tiling, tiling));
        }
        private void UpdateOffset(float offset)
        {
            _material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
        }
    }
}
