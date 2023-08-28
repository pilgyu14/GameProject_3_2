
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoSingleton<TroopsManager>, IObserver
{
    private List<IReceiver> receiverList = new List<IReceiver>();  
    [SerializeField] private int maxTroops; // 병력수 
    
    // 프로퍼티 
    public List<IReceiver> Receivers => receiverList; 

    public int MaxTroops
    {
        get => maxTroops;
        set => maxTroops = value; 
    }

    
}
