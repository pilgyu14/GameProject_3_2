using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstallManager : MonoSingleton<TreeInstallManager>, IUpdateObj
{
    public GameObject InstallPrefab;

    private Ray ray;
    RaycastHit hitInfo;
    
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
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        
        if (Physics.Raycast(ray, out hitInfo,10000000f))
        {
            if (hitInfo.transform.gameObject.layer != LayerMask.NameToLayer("Ghost"))
            {
                ghostObject.transform.position = hitInfo.point + new Vector3(0,vectorY,0);
            
                Debug.Log("Mouse World Position: " + gameObject.transform.position);
            }
            // Ray가 어떤 객체와 충돌했을 경우 그 충돌 지점의 위치를 얻습니다.
        }
    }
}
