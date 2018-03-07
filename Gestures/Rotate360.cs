using UnityEngine;
using System.Collections;
namespace WZK
{
    public class Rotate360 : MonoBehaviour
    {
        public enum State
        {
            旋转,
            按下,
            放开
        }
        private bool onDrag = false;//是否被拖拽
        public float speed = 3f;//旋转速度
        private float tempSpeed;//阻尼速度
        private float axisx;//鼠标沿水平方向移动的增量
        private float axisy;//鼠标沿竖直方向移动的增量
        private float cxy;//鼠标移动的距离
                          /// <summary>
                          /// 按下鼠标
                          /// </summary>
        void OnMouseDown()
        {
            axisx = 0f;
            axisy = 0f;
            NotificationManager<State>.Instance.DispatchEvent(State.按下);
        }
        /// <summary>
        /// 鼠标拖拽
        /// </summary>
        void OnMouseDrag()
        {
            NotificationManager<State>.Instance.DispatchEvent(State.旋转);
            onDrag = true;
            axisx = -Input.GetAxis("Mouse X");
            axisy = Input.GetAxis("Mouse Y");
            axisy = 0;
            cxy = Mathf.Sqrt(axisx * axisx + axisy * axisy);
            if (cxy == 0f)
            {
                cxy = 1f;
            }
        }
        private void OnMouseUp()
        {
            onDrag = false;
            NotificationManager<State>.Instance.DispatchEvent(State.放开);
        }
        /// <summary>
        /// 计算阻尼速度
        /// </summary>
        /// <returns></returns>
        float Rigid()
        {
            if (onDrag)
            { tempSpeed = speed; }
            else
            {
                if (tempSpeed > 0)
                {
                    tempSpeed -= speed * 2 * Time.deltaTime / cxy;
                }
                else
                {
                    tempSpeed = 0;
                }
            }
            return tempSpeed;
        }
        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            if(onDrag)gameObject.transform.Rotate(new Vector3(-axisy, axisx, 0) * Rigid(), Space.World);
        }
        private void OnDestroy()
        {
            NotificationManager<State>.Instance.RemoveAllEvent();
        }
    }
}
