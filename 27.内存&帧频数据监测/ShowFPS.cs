using UnityEngine;
using System.Collections;
namespace WZK
{
    public class ShowFPS : MonoBehaviour
    {
        [Header("Ë¢ÐÂ¼ä¸ô")]
        public float f_UpdateInterval = 0.5F;
        [Header("ÊÇ·ñÉèÖÃÖ¡Æµ")]
        public bool _isSetFrameFrequency = false;
        [Header("Ö¡Æµ")]
        public int _frameFrequency = 60;
        private float f_LastInterval;
        private int i_Frames = 0;
        private float f_Fps;
        void Start()
        {
            if (_isSetFrameFrequency) Application.targetFrameRate = _frameFrequency;
            f_LastInterval = Time.realtimeSinceStartup;
            i_Frames = 0;
        }
        void OnGUI()
        {
            if(SystemData.IsDebugBuild)GUI.Label(new Rect(10, 10, 200, 200), "FPS:" + f_Fps.ToString("f2"));
        }
        void Update()
        {
            if (SystemData.IsDebugBuild == false) return;
                ++i_Frames;
            if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
            {
                f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
                i_Frames = 0;
                f_LastInterval = Time.realtimeSinceStartup;
            }
        }
    }
}
