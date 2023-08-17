using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchBase : MonoBehaviour,ITreeElement ,IIncreaseUpgrade
{
    public EnergyType ProduceEnergyType => EnergyType.None;
    public bool UpgradeSize()
    {
        return true; 
    }

    public float ProduceEnergy()
    {
        return 1f; 
    }

    public bool IncreaseUpgrade()
    {
        return true; 
    }
}
