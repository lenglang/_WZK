using UnityEngine;
using System.Collections.Generic;
using System;
namespace WZK
{
    /// <summary>
    /// 等待动作控制
    /// 注意全局的话，要针对某个事件在OnDestroy移除，不是全局的话，移除所以事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WaitActionManager<T>
    {
        private static WaitActionManager<T> instance;
        public static WaitActionManager<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WaitActionManager<T>();
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
        public void AddWaitAction(Action action, float waitTime, T type = default(T))
        {
            WaitActionParameter wp = new WaitActionParameter(action, waitTime,type);
            _waitActionParameterList.Add(wp);
        }
        /// <summary>
        /// 移除等待动作
        /// </summary>
        /// <param name="type">动作类型</param>
        public void RemoveWaitAction(T type)
        {
            for (int i = 0; i < _waitActionParameterList.Count; i++)
            {
                if (_waitActionParameterList[i]._type == null) continue;
                if (_waitActionParameterList[i]._type.ToString() == type.ToString())
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
            instance = null;
        }
        public void FixedUpdate()
        {

            for (int i = _waitActionParameterList.Count-1; i>=0; i--)
            {
                if (Time.time - _waitActionParameterList[i]._time >= _waitActionParameterList[i]._waitTime)
                {
                    _waitActionParameterList[i]._action();
                    _waitActionParameterList.RemoveAt(i);
                }
            }
        }
        public class WaitActionParameter
        {
            public Action _action;
            public float _time;
            public float _waitTime;
            public T _type;
            public WaitActionParameter(Action action, float waitTime, T type)
            {
                _action = action;
                _waitTime = waitTime;
                _time = Time.time;
                _type = type;
            }
        }
    }
    
}
