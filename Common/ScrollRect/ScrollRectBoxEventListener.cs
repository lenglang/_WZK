using UnityEngine;
using UnityEngine.EventSystems;
namespace WZK
{
    public enum ScrollRectBoxEvent
    {
        Exit
    }
    public class ScrollRectBoxEventListener : MonoBehaviour,IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData)
        {
            NotificationManager<ScrollRectBoxEvent, GameObject>.Instance.DispatchEvent(ScrollRectBoxEvent.Exit, gameObject);
        }
    }
}
