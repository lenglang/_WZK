using UnityEngine;
using System.IO;
namespace WZK
{
    public class PhotoManager:MonoBehaviour
    {
        [Header("拍照相机")]
        public Camera _camera;
        [Header("相片宽")]
        public int _photoWidth = 786;
        [Header("相片高")]
        public int _photoHeight = 510;
        private string _headPath = "";//头路径
        private static PhotoManager _instance;
        public static PhotoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (new GameObject("PhotoManager")).AddComponent<PhotoManager>();
                }
                return _instance;
            }
        }
        public void Awake()
        {
            _headPath = Application.persistentDataPath + "/photo/";
            if (!Directory.Exists(_headPath))
            {
                Directory.CreateDirectory(_headPath);
            }
        }
        /// <summary>
        /// 照相
        /// </summary>
        public void TakePhoto(string filename)
        {
            filename += _headPath;
            Rect rect = new Rect(0, 0, _photoWidth, _photoHeight);
            _camera.gameObject.SetActive(true);
            RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 2);//2-有时候拍照,拍不到对象可以设置这个深度值
            _camera.targetTexture = rt;
            _camera.Render();
            RenderTexture.active = rt;
            Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, true);
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();
            RenderTexture.active = null;
            _camera.gameObject.SetActive(false);
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("截屏了一张照片: {0}", filename));
        }
        /// <summary>
        /// 读取照片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Texture2D LoadPhotoByIO(string fileName)
        {
            fileName += _headPath;
            //创建文件读取流
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            //释放文件读取流
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
            //创建Texture
            Texture2D texture = new Texture2D(_photoWidth, _photoHeight);
            texture.LoadImage(bytes);
            //创建 Sprite
            //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return texture;
        }
        private void OnDestroy()
        {
            _instance = null;
        }
    }
}
