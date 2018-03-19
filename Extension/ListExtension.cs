using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace WZK
{
    public static class ListExtension
    {
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                return default(T);

            return list[Random.Range(0, list.Count)];
        }
        /// <summary>
        /// 随机取列表中一个元素并剔除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetRandomRemove<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                return default(T);

            T temp = list[Random.Range(0, list.Count)];
            list.Remove(temp);

            return temp;
        }

        public static List<T> SubArray<T>(this List<T> list, int startIndex, int length = -1)
        {
            if (length == -1)
                return list.Skip(startIndex).ToList();

            return list.Skip(startIndex).Take(length).ToList();
        }
        /// <summary>
        /// 开启事件
        /// </summary>
        /// <param name="list"></param>
        public static void OpenEvent<T>(this List<T> list) where T : MonoBehaviour
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = true;
            }
        }
        /// <summary>
        /// 关闭其他事件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dg3D"></param>
        public static void CloseEvent<T>(this List<T> list) where T : MonoBehaviour
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = false;
            }
        }
        /// <summary>
        /// 关闭其他事件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dg3D"></param>
        public static void CloseOtherEvent<T>(this List<T> list,T dg3D) where T : MonoBehaviour
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = false;
            }
            dg3D.enabled = true;
        }
    }
}
