using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class TreeInstallManager : MonoSingleton<TreeInstallManager>, IUpdateObj
{
    [SerializeField]
    private GameObject InstallPrefab;

    private Camera mainCamera;
    
    private Ray ray;
    RaycastHit hitInfo;
    
    private void Awake()
    {
        UpdateManager.Instance.AddUpdateObj(this);
        mainCamera = Camera.main;
    }
    
    public void InstallObjectSet(GameObject gameObject)
    {
        if (InstallPrefab == gameObject || gameObject == null)
        {
            return;
        }
        InstallPrefab = gameObject;;
    }

    public GameObject InstallObjectGet()
    {
        return InstallPrefab;
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
        
        
        if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity, ~8))
        {
            if (hitInfo.transform.gameObject.layer != 8)
            {
                //Debug.Log(transform.gameObject.layer);
                if (Input.GetMouseButtonDown(0))
                {
                    if (InstallPrefab != null)
                    {
                        InstallPrefab = Instantiate(InstallPrefab);
                        InstallPrefab.transform.position = hitInfo.point + new Vector3(0,0,0);
                        InstallPrefab.layer = 8;
                        InstallGhost.Instance.GhostObjectSet(null,0);
                        InstallPrefab = null;
                    
                        //Debug.Log("Mouse World Position: " + InstallPrefab.transform.position);
                    }
                    //Debug.Log("안");
                }
            }
            /*else-
            {
                Debug.Log("밖");
                Debug.Log(hitInfo.transform.gameObject.layer);
            }*/
            // Ray가 어떤 객체와 충돌했을 경우 그 충돌 지점의 위치를 얻습니다.
        }
    }
}
