using UnityEngine;
namespace WZK
{
    public class ActionManager: MonoBehaviour
    {
        private void FixedUpdate()
        {
            WaitActionManager<string>.Instance.FixedUpdate();
            LoopActionManager<string>.Instance.FixedUpdate();
        }
        private void OnDestroy()
        {
            //清空
            WaitActionManager<string>.Instance.RemoveAllWaitAction();
            LoopActionManager<string>.Instance.RemoveAllLoopAction();
        }
    }
}
