using UnityEngine;
using System.Collections;
namespace WZK
{
    public static class CameraExtension
    {
        public static void OpenLayerMask(this Camera camera,int layerIndex)
        {
            camera.cullingMask |= (1 << layerIndex);
        }
        public static void CloseLayerMask(this Camera camera, int layerIndex)
        {
            camera.cullingMask &= ~(1 << layerIndex);
        }
    }
}
