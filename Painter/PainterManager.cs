using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace WZK
{
    public class PainterManager : MonoBehaviour
    {
        public enum EventType
        {
            生成图片
        }
        [Header("笔刷")]
        public GameObject _brush;
        [Header("RenderTexture")]
        public List<RenderTexture> _renderTextureList = new List<RenderTexture>();
        [Header("相机")]
        public List<Camera> _cameraList = new List<Camera>();
        [Header("色块")]
        public List<GameObject> _quadList = new List<GameObject>();
        [Header("彩色笔刷大小")]
        public float _brushScale = 1;//彩色笔刷缩放比
        [Header("绘制个数限制")]
        public int _countLimit = 50;//个数限制
        [Header("碰撞射线发射相机")]
        public Camera _camera;
        [Header("层级")]
        public string _layer = "Default";
        private List<GameObject> _brushList = new List<GameObject>();//笔刷列表
        private List<GameObject> _brushPool = new List<GameObject>();//笔刷对象池
        [HideInInspector]
        public List<Material> _materialList = new List<Material>();//材质列表
        private Camera _currentCamera;//图片相机
        [HideInInspector]
        public RenderTexture _currentRenderTexture;//当前RenderTexture
        [HideInInspector]
        public Material _currentMaterial;//当前材质
        private int _brushCount = 0;//绘制的个数
        [HideInInspector]
        public int _uvIndex;//uv索引
        private Vector3 _hitPosition = Vector3.one;
        private void Awake()
        {
            for (int i = 0; i < _quadList.Count; i++)
            {
                _materialList.Add(_quadList[i].GetComponent<MeshRenderer>().material);
            }
            SetData(0);
            _uvIndex = 1;//默认用第一套
        }
        public void SetData(int index)
        {
            _currentCamera = _cameraList[index];
            _currentRenderTexture = _renderTextureList[index];
            _currentMaterial = _materialList[index];
        }
        /// <summary>
        /// 设置笔刷颜色
        /// </summary>
        /// <param name="color"></param>
        public void SetBrushColor(Color color)
        {
            _brush.GetComponent<SpriteRenderer>().color = color;
        }
        /// <summary>
        /// 绘制
        /// </summary>
        public void DoAction(Vector3 screenPos = default(Vector3))
        {
            screenPos = Input.mousePosition;
            if (screenPos != default(Vector3))
            { _hitPosition = screenPos; }
            Vector3 uvWorldPosition = Vector3.zero;
            GameObject brushObj;
            if (HitTestUVPosition(ref uvWorldPosition))
            {
                if (_brushPool.Count > 0)
                {
                    brushObj = _brushPool[0];
                    _brushPool.RemoveAt(0);
                }
                else
                {
                    brushObj = Instantiate(_brush);
                }
                brushObj.transform.SetParent(_currentCamera.transform);
                brushObj.transform.localPosition = uvWorldPosition;
                brushObj.transform.localScale = Vector3.one * _brushScale;
                _brushList.Add(brushObj);
                brushObj.SetActive(true);
                _brushCount++;
                if (_brushCount >= _countLimit)
                {
                    SaveTexture();
                }
            }
        }
        /// <summary>
        /// 碰撞检测
        /// </summary>
        /// <param name="uvWorldPosition"></param>
        /// <returns></returns>
        bool HitTestUVPosition(ref Vector3 uvWorldPosition)
        {
            RaycastHit hit;
            Vector3 cursorPos = new Vector3(_hitPosition.x, _hitPosition.y, 0.0f);
            if (_camera == null) _camera = Camera.main;
            Ray cursorRay = _camera.ScreenPointToRay(cursorPos);
            //Debug.DrawLine(cursorRay.origin, cursorRay.direction * 100f, Color.red);
            if (Physics.Raycast(cursorRay, out hit, 200, LayerMask.GetMask(_layer)))
            {
                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (meshCollider == null || meshCollider.sharedMesh == null)
                    return false;
                Vector2 pixelUV;
                if (_uvIndex == 2)
                {
                    pixelUV = new Vector2(hit.textureCoord2.x, hit.textureCoord2.y);//获取的是二套UV
                }
                else
                {
                    pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                }
                uvWorldPosition.x = pixelUV.x - _currentCamera.orthographicSize;//To center the UV on X
                uvWorldPosition.y = pixelUV.y - _currentCamera.orthographicSize;//To center the UV on Y
                uvWorldPosition.z = -0.1f;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        public void SaveTexture()
        {
            if (_brushCount == 0) return;
            _brushCount = 0;
            RenderTexture canvasTexture = _currentRenderTexture;
            RenderTexture.active = canvasTexture;
            Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
            tex.Apply();
            RenderTexture.active = null;
            _currentMaterial.mainTexture = tex;
            for (int i = 0; i < _brushList.Count; i++)
            {
                _brushList[i].SetActive(false);
                _brushPool.Add(_brushList[i]);
            }
            _brushList.Clear();
            //StartCoroutine ("SaveTextureToFile"); //Do you want to save the texture? This is your method!
            NotificationManager<EventType>.Instance.DispatchEvent(EventType.生成图片);
        }
        /// <summary>
        /// 清楚笔刷
        /// </summary>
        public void CleanBrush()
        {
            for (int i = 0; i < _brushPool.Count; i++)
            {
                Destroy(_brushPool[i]);
            }
            _brushPool.Clear();
            for (int i = 0; i < _brushList.Count; i++)
            {
                Destroy(_brushList[i]);
            }
            _brushList.Clear();
        }
#if !UNITY_WEBPLAYER
        IEnumerator SaveTextureToFile(Texture2D savedTexture)
        {
            _brushCount = 0;
            string fullPath = System.IO.Directory.GetCurrentDirectory() + "\\UserCanvas\\";
            System.DateTime date = System.DateTime.Now;
            string fileName = "CanvasTexture.png";
            if (!System.IO.Directory.Exists(fullPath))
                System.IO.Directory.CreateDirectory(fullPath);
            var bytes = savedTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes(fullPath + fileName, bytes);
            Debug.Log("<color=orange>Saved Successfully!</color>" + fullPath + fileName);
            yield return null;
        }
#endif
    }
}
