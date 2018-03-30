using System.Collections.Generic;
using UnityEngine;
namespace WZK
{
    [System.Serializable]
    public class PositionScriptableObject : ScriptableObject
    {
        public List<TransformInformation> _positionList = new List<TransformInformation>();
    }
}
