using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventUIManager : MonoSingleton<EventUIManager>
{
    public float spider; 
    
    public List<GameObject> EventPrefab = new List<GameObject>();

    public GameObject nowEventObject;
    
    public void ShowEvent(int day)
    {
        nowEventObject = EventPrefab[day];
        Instantiate(nowEventObject);
        Time.timeScale = 0;
    }

    public void CloseEvent()
    {
        Destroy(nowEventObject);
        Time.timeScale = 1;
    }
}
