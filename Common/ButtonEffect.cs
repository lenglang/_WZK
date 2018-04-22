using UnityEngine;
using System;
namespace WZK
{
    public class ButtonEffect : MonoBehaviour
    {
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="tf">对象</param>
        /// <param name="complete">完成委托</param>
        /// <param name="n">缩放倍数</param>
        /// <param name="time">缩放时间</param>
        public static void Scale(Transform tf, Action complete = null,float n=1.2f,float time=0.12f)
        {
            //标记
            //Vector3 scale = tf.transform.localScale * n;
            //tf.DOScale(scale, time).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(delegate { if (complete != null) complete(); });
        }
    }
}
