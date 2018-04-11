using UnityEngine;
namespace WZK
{
    public class Page : MonoBehaviour
    {
        private ChoseBox1 _choseBox1;
        public void Init(ChoseBox1 choseBox1)
        {
            _choseBox1 = choseBox1;
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        { }
    }
}
