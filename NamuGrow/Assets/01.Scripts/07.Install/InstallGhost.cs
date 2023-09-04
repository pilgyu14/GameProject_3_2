using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class InstallGhost : MonoBehaviour, IUpdateObj
{
    [SerializeField]
    private GameObject ghostObject;

    private Camera mainCamera;
    
    private Ray ray;
    RaycastHit hitInfo;

    private void Awake()
    {
        UpdateManager.Instance.AddUpdateObj(this);
        mainCamera = Camera.main;
    }

    public void GhostObjectSet(GameObject gameObject)
    {
        if (ghostObject == gameObject || gameObject == null)
        {
            return;
        }

        ghostObject = gameObject;
        
    }


    public void GhostObjectHide()
    {
        if (ghostObject.activeSelf == false)
        {
            return;
        }
        Debug.Log("오브젝트 도망");
        ghostObject.SetActive(true);
    }

    public void GhostObjectShow()
    {
        if (ghostObject.activeSelf == true)
        {
            return;
        }
        Debug.Log("오브젝트 쇼타임");
        ghostObject.SetActive(false);
    }

    public void OnUpdate()
    {
        
    }

    public void OnLateUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            GhostObjectHide();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GhostObjectShow();
        }
    }

    public void OnFixedUpdate()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        
        if (Physics.Raycast(ray, out hitInfo,10000000f))
        {
            if (hitInfo.transform.gameObject.layer != LayerMask.NameToLayer("Ghost"))
            {
                ghostObject.transform.position = hitInfo.point;
            
                Debug.Log("Mouse World Position: " + gameObject.transform.position);
            }
            // Ray가 어떤 객체와 충돌했을 경우 그 충돌 지점의 위치를 얻습니다.
        }
    }
}