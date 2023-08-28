using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        Cashing();
    }

    private void Cashing()
    {
        cam ??= GetComponent<CinemachineVirtualCamera>();
    }
   
    
}
