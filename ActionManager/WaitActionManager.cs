using UnityEngine;
using System.Collections.Generic;
using System;
namespace WZK
{
    /// <summary>
    /// 等待动作控制
    /// </summary>
    public class WaitActionManager:MonoBehaviour
    {
        private static WaitActionManager instance;
        public static WaitActionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject("WaitActionManager")).AddComponent<WaitActionManager>();
                }
                return instance;
            }
        }
        /// <summary>
        /// 等待动作列表
        /// </summary>
        private List<WaitActionParameter> _waitActionParameterList = new List<WaitActionParameter>();
        /// <summary>
        /// 添加等待动作
        /// </summary>
        /// <param name="type">动作类型</param>
        /// <param name="action">动作</param>
        /// <param name="waitTime">等待时间</param>
        public void AddWaitAction(Action action, float waitTime, string type="")
        {
            WaitActionParameter wp = new WaitActionParameter(action, waitTime,type);
            _waitActionParameterList.Add(wp);
        }
        /// <summary>
        /// 移除等待动作
        /// </summary>
        /// <param name="type">动作类型</param>
        public void RemoveWaitAction(string type)
        {
            for (int i = 0; i < _waitActionParameterList.Count; i++)
            {
                if (_waitActionParameterList[i]._type== type)
                {
                    _waitActionParameterList.RemoveAt(i);
                    break;
                }
            }
        }
        /// <summary>
        /// 移除所有等待动作
        /// </summary>
        public void RemoveAllWaitAction()
        {
            _waitActionParameterList.Clear();
        }
        public void FixedUpdate()
        {
            float time = Time.time;
            for (int i = _waitActionParameterList.Count-1; i>=0; i--)
            {
                if (time - _waitActionParameterList[i]._time >= _waitActionParameterList[i]._waitTime)
                {
                    _waitActionParameterList[i]._action();
                    _waitActionParameterList.RemoveAt(i);
                }
            }
        }
        private void OnDestroy()
        {
            instance = null;
        }
        public class WaitActionParameter
        {
            public Action _action;
            public float _time;
            public float _waitTime;
            public string _type;
            public WaitActionParameter(Action action, float waitTime, string type)
            {
                _action = action;
                _waitTime = waitTime;
                _time = Time.time;
                _type = type;
            }
        }
    }
    
}
