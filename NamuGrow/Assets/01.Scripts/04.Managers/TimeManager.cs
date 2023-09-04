using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private class TimedFunction
    {
        public Action Function;
        public float Interval;
        public float LastCalledTime;
    }

    //private readonly List<TimedFunction> timedFunctions = new List<TimedFunction>();
    private readonly Dictionary<string, TimedFunction> timedFunctionsDic = new Dictionary<string, TimedFunction>();


    /// <summary>
    /// 시간마다 함수 호출
    /// Seconds 가 0이면 한 번만 실행 
    /// </summary>
    /// <param name="function"></param>
    /// <param name="seconds"></param>
    public void CallEventPerTime(string _key, Action function, float seconds)
    {
        TimedFunction timedFunction = new TimedFunction
        {
            Function = function,
            Interval = seconds,
            LastCalledTime = Time.time
        };
        timedFunctionsDic.Add(_key, timedFunction);
    }

    public void RemoveCallEvent(string _key)
    {
        if (timedFunctionsDic.ContainsKey(_key) == true)
        {
            timedFunctionsDic.Remove(_key);
            return; 
        }
        Debug.Log(_key + "TImeManager 키가 없음");
    }
    private void Update()
    {
        float currentTime = Time.time;

        foreach (var _keyFunction in timedFunctionsDic)
        {
            TimedFunction timedFunction = _keyFunction.Value;

            if (currentTime - timedFunction.LastCalledTime >= timedFunction.Interval)
            {
                timedFunction.Function.Invoke();
                timedFunction.LastCalledTime = currentTime;

                // If it's a CallAfter function, remove it after it's called
                if (timedFunction.Interval == 0)
                {
                    timedFunctionsDic.Remove(_keyFunction.Key);
                }
            }
        }
    }
}