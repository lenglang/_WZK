using UnityEngine;
namespace WZK
{
    public enum ExampleType
    {
        分数
    }
    public class NotificationExample : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            NotificationManager<ExampleType>.Instance.AddEventListener(ExampleType.分数, delegate { Debug.Log("666"); });
            NotificationManager<ExampleType>.Instance.AddEventListener(ExampleType.分数, OnComplete1);
            NotificationManager<ExampleType, NotificationContent>.Instance.AddEventListener(ExampleType.分数, OnComplete2);
            NotificationContent nc = new NotificationContent();
            nc._sender = this.gameObject;
            nc._age = 20;
            nc._name = "宝宝";
            NotificationManager<ExampleType>.Instance.DispatchEvent(ExampleType.分数);
            NotificationManager<ExampleType, NotificationContent>.Instance.DispatchEvent(ExampleType.分数, nc);
        }
        private void OnComplete1(ExampleType et)
        {
            Debug.Log("分数更新");
        }
        private void OnComplete2(ExampleType et, NotificationContent data)
        {
            Debug.Log("获取数据:" + data.ToString());
        }
    }
}
