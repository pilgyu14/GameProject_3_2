using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    private static Camera mainCam;

    public static Camera MainCam
    {
        get
        {
            if (mainCam == null)
            {
                mainCam = Camera.main;
            }

            return mainCam;
        }
    }

    public static Vector3 ClickPos
    {
        get
        {
            Vector3 _worldPos = new Vector3(); 
            Vector3 mousePosition = Input.mousePosition;
            // 카메라에서 마우스 클릭 지점까지의 거리를 설정하여 월드 좌표를 구함
            Ray _ray =MainCam.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(_ray, out RaycastHit _hitInfo, float.MaxValue, 1 << 6))
            {
                _worldPos = _hitInfo.point; 
            }
            else
            {
                Debug.LogError("충돌 지점이 없음 ");
            }
            return _worldPos; 
        }
    }
}