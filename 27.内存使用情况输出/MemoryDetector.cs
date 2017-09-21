using System.Collections;
using UnityEngine;
namespace WZK
{
    /// <summary> 
    /// 内存检测器
    /// </summary> 
    public class MemoryDetector : MonoBehaviour
    {
        void OnGUI()
        {
            if (SystemData.IsDebugBuild == false) return;
            string sUserMemory = "";
            sUserMemory += "Reserved:" + Profiler.GetTotalReservedMemory() / 1000000 + "M" + "\n";
            sUserMemory += "UnUsedReserved:" + Profiler.GetTotalUnusedReservedMemory() / 1000000 + "M" + "\n";
            sUserMemory += "AllMemory:" + Profiler.GetTotalAllocatedMemory() / 1000000 + "M" + "\n";
            sUserMemory += "MonoUsed:" + Profiler.GetMonoUsedSize() / 1000000 + "M";
            GUILayout.Box(sUserMemory);
        }
    }
}