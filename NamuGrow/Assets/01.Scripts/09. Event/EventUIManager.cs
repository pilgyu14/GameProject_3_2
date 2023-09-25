using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventUIManager : MonoSingleton<EventUIManager>
{
    public float spider;
    public float bee;
    public float ant;
    
    public List<GameObject> EventPrefab = new List<GameObject>();

    public GameObject nowEventObject;

    private EnergyManager energyManager;

    private void Awake()
    {
        spider = 0.5f;
        bee = 0.6f;
        ant = 0.55f;

        energyManager = EnergyManager.Instance;
    }

    public void ShowEvent(int day)
    {
        nowEventObject = Instantiate(EventPrefab[day]);
        Time.timeScale = 0;
    }

    public void CloseEvent(float spiderAdd, float beeAdd, float antAdd, int sap, int sweetsap, int acorn, int kiwi)
    {
        if (-sap > energyManager.Sap || -sweetsap > energyManager.SweetSap || -acorn > energyManager.Acorn || -kiwi > energyManager.Kiwi)
        {
            return;
        }

        energyManager.Sap += sap;
        energyManager.SweetSap += sweetsap;
        energyManager.Acorn += acorn;
        energyManager.Kiwi += kiwi;
        
        Destroy(nowEventObject);
        spider += spiderAdd;
        bee += beeAdd;
        ant += antAdd;
        Time.timeScale = 1;
    }
}
