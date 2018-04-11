using UnityEngine;
using UnityEngine.EventSystems;
namespace WZK
{
    /// <summary>
    /// 按下事件
    /// </summary>
    public class DownUpEvent : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public delegate void VoidDelegate(PointerEventData eventData, GameObject obj, DownUpEvent etl);
        public VoidDelegate _downAction;
        public VoidDelegate _upAction;
        public static DownUpEvent Get(GameObject go)
        {
            DownUpEvent listener = go.GetComponent<DownUpEvent>();
            if (listener == null) listener = go.AddComponent<DownUpEvent>();
            return listener;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_downAction != null) _downAction(eventData, gameObject, this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_upAction != null) _upAction(eventData, gameObject, this);
        }
    }
}
