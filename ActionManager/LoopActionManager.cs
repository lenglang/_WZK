using UnityEngine;
using System;
using System.Collections.Generic;
namespace WZK
{
    /// <summary>
    /// 循环动作控制
    /// 注意全局的话，要针对某个事件在OnDestroy移除，不是全局的话，移除所有事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoopActionManager<T>
    {
        private static LoopActionManager<T> instance;
        public static LoopActionManager<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoopActionManager<T>();
                }
                return instance;
            }
        }
        /// <summary>
        /// 循环动作列表
        /// </summary>
        private List<LoopActionParameter> _loopActionParameterList = new List<LoopActionParameter>();
        /// 添加循环动作
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="interval">循环间隔</param>
        /// <param name="type">该动作类型</param>
        /// <param name="isDoNow">是否马上执行放还是隔几秒后执行，默认马上执行</param>
        /// <param name="interval2">第一次间隔结束后，是否改变之后的间隔时间，默认0即不改变，其他值为下次间隔时间</param>
        /// <param name="add">增加时间</param>
        /// <param name="times">循环次数，默认-1即无限循环</param>
        public void AddLoopAction(Action action, float interval, T type = default(T), bool isDoNow = true, float interval2 = 0, float add = 0, int times = -1)
        {
            LoopActionParameter lp = new LoopActionParameter(action, interval, type, times, interval2, add);
            _loopActionParameterList.Add(lp);
            if (isDoNow) action();
        }
        /// <summary>
        /// 移除某个循环动作
        /// </summary>
        /// <param name="actionName"></param>
        public void RemoveLoopAction(T type)
        {
            for (int i = 0; i < _loopActionParameterList.Count; i++)
            {
                if (_loopActionParameterList[i]._type == null) continue;
                if (_loopActionParameterList[i]._type.ToString() == type.ToString())
                {
                    _loopActionParameterList.RemoveAt(i);
                    break;
                }
            }
        }
        /// <summary>
        /// 重置时间
        /// </summary>
        /// <param name="type"></param>
        public void ResetTime(T type)
        {
            for (int i = 0; i < _loopActionParameterList.Count; i++)
            {
                if (_loopActionParameterList[i]._type == null) continue;
                if (_loopActionParameterList[i]._type.ToString() == type.ToString())
                {
                    _loopActionParameterList[i]._time = Time.time;
                    break;
                }
            }
        }
        /// <summary>
        /// 移除所有循环动作
        /// </summary>
        public void RemoveAllLoopAction()
        {
            _loopActionParameterList.Clear();
            instance = null;
        }
        public void FixedUpdate()
        {
            LoopActionParameter lp;
            for (int i = _loopActionParameterList.Count - 1; i >= 0; i--)
            {
                lp = _loopActionParameterList[i];
                if (Time.time - lp._time >= lp._interval)
                {
                    lp._countTimes++;
                    lp._action();
                    lp._time = Time.time;
                    if (lp._interval2 != 0 && lp._countTimes == 1) lp._interval = lp._interval2;
                    if (lp._add != 0 && lp._countTimes != 1) lp._interval += lp._add;
                    if (lp._times > 0)
                    {
                        lp._times--;
                        if (lp._times == 0) _loopActionParameterList.RemoveAt(i);
                    }
                }
            }
        }
        /// <summary>
        /// 循环动作参数类
        /// </summary>
        public class LoopActionParameter
        {
            public Action _action;
            public float _time;
            public float _interval;
            public int _times;
            public float _interval2;
            public float _add;
            public float _countTimes = 0;
            public T _type;
            public LoopActionParameter(Action action, float interval, T type, int times, float interval2, float add)
            {
                _action = action;
                _interval = interval;
                _type = type;
                _times = times;
                _interval2 = interval2;
                _add = add;
                _time = Time.time;
            }
        }
    }
}

