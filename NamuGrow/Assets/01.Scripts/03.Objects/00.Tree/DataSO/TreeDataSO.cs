using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tree/TreeDataSO")]
public class TreeDataSO : ScriptableObject
{
    public TreeType treeType;
    public EnergyType productEnergyType; // 자원 생산 타입 
    public List<EnergyType> productEnergyTypeList =new List<EnergyType>(); // 자원 생산 타입 
    public TreeLevel maxLevel; // 최대 레벨 
    public TreeLevel curLevel; // 현재 레벨

    public List<TreeByLevelSO> treeByLevelList = new List<TreeByLevelSO>(); // 레벨에 따른 능력치 

    // 현재 나무 데이터 
    public TreeByLevelSO CurLevelSoData => treeByLevelList.Find((x) => x.level == curLevel);

    /// <summary>
    /// MaxLevel이 되면 false 반환 
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public bool LevelUp(int _value = 1)
    {
        ++curLevel;
        return curLevel < maxLevel;
    }
}