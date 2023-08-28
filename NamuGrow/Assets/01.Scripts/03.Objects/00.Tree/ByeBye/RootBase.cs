using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBase : AbTreeElement<OakTree> ,IIncreaseUpgrade, IProduceEnergy
{
    public override EnergyType ProduceEnergyType => EnergyType.None;
    public override bool Upgrade()
    {
        return true; 
    }

    public float ProduceEnergy()
    {
       // float _energy = // 현재 레벨 * 기준 나무 생산량 ;
        //return _energy; 
        return 1f; 
    }

    public bool IncreaseUpgrade()
    {
        return true; 
    }
}
