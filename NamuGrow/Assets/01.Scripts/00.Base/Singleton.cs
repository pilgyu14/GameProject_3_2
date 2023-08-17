using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 클래스를 싱글톤화
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class, new()
{
    public static T Instance
    {
        get
        {
            return instance.Value;
        }
    }

    private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

}