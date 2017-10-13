using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WZK
{
    public class ScrollRectManager : MonoBehaviour
    {
        [Header("Item")]
        public GameObject _item;
        [Header("Item容器")]
        public Transform _itemParent;
        [Header("Sprite列表")]
        public List<Sprite> _spriteList;
        [Header("ScrollRect")]
        public ScrollRect _scrollRect;
        /// <summary>
        /// Item列表
        /// </summary>
        private List<GameObject> _itemList;
        private void Awake()
        {
            if (_spriteList.Count != 0)
            {
                for (int i = 0; i < _spriteList.Count; i++)
                {
                    CreateItem(_spriteList[i]);
                }
            }
        }
        /// <summary>
        /// 创建Item
        /// </summary>
        /// <param name="sprite"></param>
        public void CreateItem(Sprite sprite)
        {
            GameObject item = Instantiate(_item);
            item.SetActive(true);
            item.GetComponent<Image>().sprite = sprite;
            item.GetComponent<Transform>().SetParent(_itemParent);
            item.name = sprite.name;
        }
        private void Start()
        {
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.AddEventListener(ScrollRectItemEvent.Down, DownItem);
            NotificationManager<ScrollRectBoxEvent, GameObject>.Instance.AddEventListener(ScrollRectBoxEvent.Exit, ExitBox);
            NotificationManager<ScrollRectItemEvent, GameObject>.Instance.AddEventListener(ScrollRectItemEvent.UP, ItemUp);
        }

        private void ItemUp(GameObject arg0)
        {
            Debug.Log("aaaaa");
        }

        /// <summary>
        /// 按下Item
        /// </summary>
        /// <param name="arg0"></param>
        private void DownItem(GameObject arg0)
        {
            Debug.Log("按下Item");
        }
        /// <summary>
        /// 离开Item栏
        /// </summary>
        /// <param name="arg0"></param>
        private void ExitBox(GameObject arg0)
        {
            _scrollRect.enabled = false;
        }
    }
}
