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
        /// 开启3D拖拽
        /// </summary>
        /// <param name="list"></param>
        public static void OpenDragEvent(this List<DragGestures3D> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = true;
            }
        }
        /// <summary>
        /// 关闭其他3D拖拽
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dg3D"></param>
        public static void CloseOtherDragEvent(this List<DragGestures3D> list,DragGestures3D dg3D)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = false;
            }
            dg3D.enabled = true;
        }
        /// <summary>
        /// 开启2D拖拽
        /// </summary>
        /// <param name="list"></param>
        public static void OpenDragEvent(this List<DragGestures2D> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = true;
            }
        }
        /// <summary>
        /// 关闭其他2D拖拽
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dg2D"></param>
        public static void CloseOtherDragEvent(this List<DragGestures2D> list, DragGestures2D dg2D)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].enabled = false;
            }
            dg2D.enabled = true;
        }
    }
}
