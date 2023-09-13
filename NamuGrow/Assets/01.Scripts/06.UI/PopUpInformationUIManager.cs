using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInformationUIManager : MonoBehaviour, IUpdateObj
{
    public GameObject uiPrefab; // 생성할 UI 프리팹
    private GameObject currentUI; // 현재 UI 인스턴스
    public Camera mainCamera; // 카메라를 지정하기 위한 변수

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 화면을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭한 위치를 스크린 좌표로 가져오기
            Vector3 screenClickPosition = Input.mousePosition;

            // 스크린 좌표를 월드 좌표로 변환
            Vector3 worldClickPosition = mainCamera.ScreenToWorldPoint(screenClickPosition);
            
            // X 축 값을 카메라 위치에서 빼줍니다.
            worldClickPosition.x -= mainCamera.transform.position.x;

            // UI를 생성하고 변환된 월드 좌표로 이동
            CreateUI(worldClickPosition);
        }
    }

    // UI를 생성하는 메서드
    private void CreateUI(Vector3 position)
    {
        // 이전 UI가 있으면 제거
        if (currentUI != null)
        {
            Destroy(currentUI);
        }

        // UI 프리팹을 인스턴스화하고 계산된 월드 좌표로 이동
        currentUI = Instantiate(uiPrefab, position, Quaternion.identity);

        // UI를 Canvas 하위로 이동 (Canvas를 사용하여 UI를 렌더링)
        currentUI.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    public void OnUpdate()
    {
        throw new NotImplementedException();
    }

    public void OnLateUpdate()
    {
        throw new NotImplementedException();
    }

    public void OnFixedUpdate()
    {
        throw new NotImplementedException();
    }
}