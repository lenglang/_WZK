//=======================================================
// 作者：王则昆
// 描述：选择样式
//=======================================================
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
namespace WZK
{
    public class ChoseStyle : ChoseBoxBase
    {
        public static ChoseStyle Instance = null;
        [Header("砖块图")]
        public List<Sprite> _brickSprite;
        [Header("墙图")]
        public List<Sprite> _wallSprite;
        [Header("门窗")]
        public List<Sprite> _doorWindowSprite;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            //测试
            ChoseStyle.Instance.CreateItem(_doorWindowSprite);
            ChoseStyle.Instance._hit = GameObject.Find("Cube");
            ChoseStyle.Instance._putSuccess = PutSuccess;
        }
        /// <summary>
        /// 创建Item
        /// </summary>
        /// <param name="list"></param>
        private void CreateItem(List<Sprite> list)
        {
            GameObject item;
            Vector2 p = Vector2.zero;
            for (int i = 0; i < list.Count; i++)
            {
                item = Instantiate(_item, _container);
                item.SetActive(true);
                p.y = (1 - i) * _moveDis;
                item.transform.localPosition = p;
                item.name = list[i].name;
                item.transform.Find("Image").GetComponent<Image>().sprite = list[i];
                _itemList.Add(item);
            }
            _downStateAction = DownItem;
            _upsStateAction = UpItem;
            _createDragObjectAction = CreateDragObject;
            Init();
        }
        /// <summary>
        /// 放置成功
        /// </summary>
        /// <param name="obj"></param>
        private void PutSuccess(string n)
        {
            Debug.Log(n);
            ChoseStyle.Instance.HidePanel();
        }
        /// <summary>
        /// 创建拖拽物体
        /// </summary>
        /// <param name="obj"></param>
        private void CreateDragObject(GameObject obj)
        {
            _dragObject = Instantiate(obj,transform);
            _dragObject.transform.localScale *= 1.5f;
            _dragObject.name = obj.name;
            _dragObject.transform.Find("选中").gameObject.SetActive(false);
        }
        private void DownItem(GameObject obj)
        {
            obj.transform.Find("选中").gameObject.SetActive(true);
        }
        private void UpItem(GameObject obj)
        {
            obj.transform.Find("选中").gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            Instance = null;
        }
    }
}
