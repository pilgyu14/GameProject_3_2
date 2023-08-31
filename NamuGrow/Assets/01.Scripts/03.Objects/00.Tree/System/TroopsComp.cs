using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  병력수 증가 
/// </summary>
public class TroopsComp : AbTreeSystemComp, IObserver
{
    private int troopsAmount;

    public int TroopsAmount => treeDataSO.CurLevelSoData.addTroops; 
    private void Start()
    {
        AddObserver(TroopsManager.Instance);
    }

    private void OnDestroy()
    {
        RemoveObserver(TroopsManager.Instance);
    }

    public void AddObserver(IReceiver _receiver)
    {
        if (!Receivers.Contains(_receiver))
        {
            Receivers.Add(_receiver);
        }
    }
    
    public void RemoveObserver(IReceiver _receiver)
    {
        if (Receivers.Contains(_receiver))
        {
            Receivers.Remove(_receiver);
        }
    }

    private List<IReceiver> receiverList = new List<IReceiver>();
    public List<IReceiver> Receivers => receiverList;
    public override void UpdateUpgrade()
    {
    }
}
