//=======================================================
// 作者：王则昆
// 描述：
//=======================================================
using UnityEngine;
using System.Collections;
namespace WZK
{
    public class WaveMove : MonoBehaviour
    {
        public float _i;
        public float _add=0.1f;
        public float _r=15f;
        public float _fps = 0;
        private void FixedUpdate()
        {
            _fps++;
            _i += _add;
            if (_fps >= 2)
            {
                _fps = 0;
                return;
            }
            transform.SetLocalAngleZ(_r * Mathf.Sin(_i));
        }
    }
}
