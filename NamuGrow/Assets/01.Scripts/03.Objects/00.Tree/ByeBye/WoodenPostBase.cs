using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenPostBase : AbTreeElement<OakTree>
{
    public override EnergyType ProduceEnergyType => EnergyType.None;
    public override bool Upgrade()
    {
        return true; 
    }
    

}
