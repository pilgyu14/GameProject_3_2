using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook  cam;
    public float rotationSpeed = 60.0f;
    public float zoomSpeed = 10.0f;
    private void Awake()
    {
        Cashing();
    }

    private void Cashing()
    {
        cam ??= GetComponent<CinemachineFreeLook >();
    }

    private void Update()
    {
        // 회전
        if (Input.GetKey(KeyCode.Q))
        {
            cam.m_XAxis.Value += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            cam.m_XAxis.Value -= rotationSpeed * Time.deltaTime;
        }

        // 확대 및 축소
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        cam.m_Orbits[0].m_Radius -= zoomInput * zoomSpeed;
        cam.m_Orbits[1].m_Radius -= zoomInput * zoomSpeed;

        // 확대 범위 제한 (원하는 값에 맞게 조정)
        cam.m_Orbits[0].m_Radius = Mathf.Clamp(cam.m_Orbits[0].m_Radius, 5f, 20f);
        cam.m_Orbits[1].m_Radius = Mathf.Clamp(cam.m_Orbits[1].m_Radius, 5f, 20f);
    }
}
