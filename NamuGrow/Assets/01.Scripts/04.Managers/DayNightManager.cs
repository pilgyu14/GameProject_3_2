using System;
using UnityEngine;
using System.Collections.Generic;

public class DayNightManager : MonoBehaviour, IUpdateObj
{
    public Light sun; // 낮과 밤을 제어할 라이트 컴포넌트
    public List<float> dayDurationsInSeconds = new List<float>(); // 낮의 지속 시간 (초) 리스트
    public List<float> nightDurationsInSeconds = new List<float>(); // 밤의 지속 시간 (초) 리스트
    public float lightIntensityChangeDuration = 5.0f; // 빛의 강도가 서서히 변하는 데 걸리는 시간 (초)
    public Material daySkybox; // 낮 스카이박스
    public Material nightSkybox; // 밤 스카이박스

    private float timer = 0.0f;
    private bool isDay = true;
    private int currentDay = 0;
    private float startIntensity;
    private float targetIntensity;
    private float intensityChangeStartTime;
    private bool isSkyboxChanging = false;

    private void Awake()
    {
        UpdateManager.Instance.AddUpdateObj(this);
        
        // 초기 설정을 첫 번째 낮으로 설정
        isDay = true;
        RenderSettings.skybox = daySkybox;
        sun.intensity = 1.5f;
        //currentDay = 0;
    }

    private void Start()
    {
        
    }

    public void OnUpdate()
    {
        
    }

    public void OnLateUpdate()
    {
    }

    public void OnFixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            // 현재가 낮이면 밤으로 변경
            if (isDay)
            {
                isDay = false;
                startIntensity = sun.intensity;
                targetIntensity = 0.2f;
                startIntensity = 1.5f;
                intensityChangeStartTime = Time.time;
                timer = nightDurationsInSeconds[currentDay];
            }
            // 현재가 밤이면 다음 낮으로 변경
            else
            {
                isDay = true;
                startIntensity = sun.intensity;
                targetIntensity = 1.5f;
                startIntensity = 0.2f;
                intensityChangeStartTime = Time.time;

                // 다음 낮과 밤의 시간을 가져옵니다.
                currentDay = (currentDay + 1) % Mathf.Min(dayDurationsInSeconds.Count, nightDurationsInSeconds.Count);
                Debug.Log(currentDay);
                timer = dayDurationsInSeconds[currentDay];
            }

            // 스카이박스 변경을 허용
            isSkyboxChanging = true;
        }

        // 빛의 강도 서서히 변화 적용
        float elapsed = Time.time - intensityChangeStartTime;
        if (elapsed < lightIntensityChangeDuration)
        {
            float t = elapsed / lightIntensityChangeDuration;
            sun.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
        }
        else
        {
            sun.intensity = targetIntensity;

            // 스카이박스 변경 허용 상태에서 스카이박스 변경
            if (isSkyboxChanging)
            {
                RenderSettings.skybox = isDay ? daySkybox : nightSkybox;
                isSkyboxChanging = false;
            }
        }
    }

    private IEnumerator RotateUI()
    {
        while (true)
        {
            if (isRotating)
            {
                // 회전 애니메이션
                if (rotationTimer < rotationDuration)
                {
                    float t = rotationTimer / rotationDuration;
                    nightIcon.localRotation = Quaternion.Lerp(initialNightRotation, Quaternion.Euler(0f, 0f, 0f), t);
                    dayIcon.localRotation = Quaternion.Lerp(initialDayRotation, Quaternion.Euler(0f, 0f, 180f), t);
                    rotationTimer += Time.deltaTime;
                }
                else
                {
                    // 서서히 회전이 완료되면 대기 타이머를 시작하고 회전 상태를 변경
                    delayTimer += Time.deltaTime;
                    if (delayTimer >= delayBeforeInstantRotation)
                    {
                        isRotating = false;
                    }
                }
            }
            else
            {
                // 한 번에 아이콘을 회전시킴
                if (isNight)
                    SetDay();
                else
                    SetNight();
            }

            yield return null;
        }
    }
}