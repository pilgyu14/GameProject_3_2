using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class DayNightManager : MonoSingleton<DayNightManager>, IUpdateObj
{
    public Light sun; // 낮과 밤을 제어할 라이트 컴포넌트
    public List<float> dayDurationsInSeconds = new List<float>(); // 낮의 지속 시간 (초) 리스트
    public List<float> nightDurationsInSeconds = new List<float>(); // 밤의 지속 시간 (초) 리스트
    public float lightIntensityChangeDuration = 5.0f; // 빛의 강도가 서서히 변하는 데 걸리는 시간 (초)
    public Material daySkybox; // 낮 스카이박스
    public Material nightSkybox; // 밤 스카이박스

    private float timer = 0.0f;
    public bool isDay = true;
    private bool isRotating;

    private int CurrentDay;
    
    public int currentDay 
    {
        set
        {
            CurrentDay = value;
            Invoke("DayUISet", 1.5f);
        }
        get
        {
            return CurrentDay;
        }
    }
    
    //public List<> 

    private float startIntensity;
    private float targetIntensity;
    private float intensityChangeStartTime;
    private bool isSkyboxChanging = false;
    
    public RectTransform dayNightIcon;
    public float rotationDuration = 2.0f; // 회전 애니메이션의 지속 시간 (초)
    public float finalRotationDuration = 0.5f; // 확 변하는 애니메이션의 지속 시간 (초)

    private float rotationTimer = 0.0f;
    private Quaternion initialRotation;
    private bool isUIRotating = false;

    public TextMeshProUGUI dayTimer;

    public List<EventSO> EventSOList = new List<EventSO>();

    private EventUIManager eventUIManager;
    
    private void Awake()
    {
        UpdateManager.Instance.AddUpdateObj(this);
        eventUIManager = EventUIManager.Instance;
        
        // 초기 설정을 첫 번째 낮으로 설정
        isDay = false;
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
                SetNight();
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
                timer = dayDurationsInSeconds[currentDay];
                SetDay();
                
                eventUIManager.ShowEvent(currentDay - 1);
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

    private IEnumerator RotateToNightOrDay(bool isDayNight)
    {
        //this.isDay = isDayNight;
        isRotating = true;

        // 천천히 회전
        float startRotation = dayNightIcon.localRotation.eulerAngles.z;
        float targetRotation = isDayNight ? 180f : 0f;
        float elapsedTime = 0f;

        // 천천히 회전
        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            dayNightIcon.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막에 확 변하는 애니메이션
        float finalRotationStartTime = Time.time;
        float finalRotationEndTime = finalRotationStartTime + finalRotationDuration;
        while (Time.time < finalRotationEndTime)
        {
            float t = (Time.time - finalRotationStartTime) / finalRotationDuration;
            dayNightIcon.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(targetRotation, startRotation, t));
            yield return null;
        }

        // 애니메이션 완료
        dayNightIcon.localRotation = Quaternion.Euler(0f, 0f, targetRotation);
        isUIRotating = false;
    }
    
    private void SetDay()
    {
        StartCoroutine(RotateToNightOrDay(false)); // 아침 방향으로 회전
    }

    private void SetNight()
    {
        StartCoroutine(RotateToNightOrDay(true)); // 밤 방향으로 회전
    }

    private void DayUISet()
    {
        dayTimer.text = "Day " + currentDay.ToString();
        //Debug.Log(currentDay);
    }

    private void EventChecker()
    {
        EventManager.Instance.EventSet(EventSOList[currentDay]);
    }
}