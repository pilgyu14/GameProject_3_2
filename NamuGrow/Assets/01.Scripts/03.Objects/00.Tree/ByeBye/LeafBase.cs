using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBase : AbTreeElement<OakTree> , IProduceEnergy
{
    public override EnergyType ProduceEnergyType { get; }
    public override bool Upgrade()
    {
        // 크기, 양 키우기 
        return true; 
    }

    public float ProduceEnergy()
    {
        // 빛 에너지 얻기 
        return 1; 
    }
}
