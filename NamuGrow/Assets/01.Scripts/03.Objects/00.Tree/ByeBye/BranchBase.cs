using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchBase : AbTreeElement<OakTree> , IIncreaseUpgrade
{
    // 나뭇가지 자라는 위치 리스트 
    private List<Transform> branchGrowingPartList = new List<Transform>(); 
    
    public override EnergyType ProduceEnergyType => EnergyType.None;

    public override bool Upgrade()
    {
        // 사이즈 업 
        // 현재 에너지 체크 
        return true; 
    }
    
    /// <summary>
    /// 나뭇가지 증가 
    /// </summary>
    /// <returns></returns>
    public bool IncreaseUpgrade()
    {
        return true; 
    }
    
    // 가지 늘리기 
    // 나뭇잎 달기 

}
