using UnityEngine;
using UnityEngine.EventSystems;
namespace WZK
{
    public enum ScrollRectItemEvent
    {
        Click,
        Down,
        Enter,
        Exit,
        UP
    }
    public class ScrollRectItemEventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.DispatchEvent(ScrollRectItemEvent.Click, gameObject);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.DispatchEvent(ScrollRectItemEvent.Down, gameObject);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.DispatchEvent(ScrollRectItemEvent.Enter, gameObject);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.DispatchEvent(ScrollRectItemEvent.Exit, gameObject);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.DispatchEvent(ScrollRectItemEvent.UP, gameObject);
        }
    }
}
