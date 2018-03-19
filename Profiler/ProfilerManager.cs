using UnityEngine;
namespace WZK
{
    public class ProfilerManager : MonoBehaviour
    {
        private float f_UpdateInterval = 0.1f;
        private float f_LastInterval;
        private int i_Frames = 0;
        private float f_Fps;
        private void Start()
        {
            f_LastInterval = Time.realtimeSinceStartup;
            i_Frames = 0;
        }
        private void OnGUI()
        {
            ++i_Frames;
            if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
            {
                f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
                i_Frames = 0;
                f_LastInterval = Time.realtimeSinceStartup;
            }
            string sUserMemory = "";
            sUserMemory += "AllMemory:" + UnityEngine.Profiling.Profiler.GetTotalAllocatedMemory() / 1000000 + "M" + "\n";
            sUserMemory += "MonoUsed:" + UnityEngine.Profiling.Profiler.GetMonoUsedSize() / 1000000 + "M" + "\n";
            sUserMemory += "FPS:" + f_Fps.ToString("f2");
            GUILayout.Box(sUserMemory);
        }
    }
}
