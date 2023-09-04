using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstallManager : MonoSingleton<TreeInstallManager>, IUpdateObj
{
    public GameObject InstallPrefab;

    private void Awake()
    {
        UpdateManager.Instance.AddUpdateObj(this);
    }

    public void OnUpdate()
    {
    }

    public void OnLateUpdate()
    {
    }

    public void OnFixedUpdate()
    {
    }
}
