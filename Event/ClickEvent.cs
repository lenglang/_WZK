using UnityEngine;
using UnityEngine.EventSystems;
namespace WZK
{
    /// <summary>
    /// 按下事件
    /// </summary>
    public class ClickEvent : MonoBehaviour,IPointerClickHandler
    {
        public delegate void VoidDelegate(PointerEventData evenData, GameObject obj, ClickEvent etl);
        public VoidDelegate _action;
        public static ClickEvent Get(GameObject go)
        {
            ClickEvent listener = go.GetComponent<ClickEvent>();
            if (listener == null) listener = go.AddComponent<ClickEvent>();
            return listener;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("点击");
            if (_action != null) _action(eventData, gameObject, this);
        }
    }
}
