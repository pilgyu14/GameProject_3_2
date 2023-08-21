using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoSingleton<ClickManager>, IUpdateObj
{
    private void Awake()
    {
        
    }

    public void OnUpdate()
    {
        // 클릭 감지 
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    public void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
