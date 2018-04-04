using UnityEngine;
using UnityEngine.EventSystems;
namespace WZK
{
    /// <summary>
    /// 按下事件
    /// </summary>
    public class DownEvent : MonoBehaviour,IPointerDownHandler
    {
        public delegate void VoidDelegate(PointerEventData evenData, GameObject obj, DownEvent etl);
        public VoidDelegate _action;
        public static DownEvent Get(GameObject go)
        {
            DownEvent listener = go.GetComponent<DownEvent>();
            if (listener == null) listener = go.AddComponent<DownEvent>();
            return listener;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_action != null) _action(eventData, gameObject, this);
        }
    }
}
