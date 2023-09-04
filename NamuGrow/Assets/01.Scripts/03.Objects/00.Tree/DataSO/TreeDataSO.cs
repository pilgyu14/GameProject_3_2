using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/Tree/TreeDataSO")]
public class TreeDataSO : ScriptableObject
{
    
    public TreeType treeType;
    public string spriteAddress;
    public AbTreeBase treePrefab; 
    
    public TreeAbilityType treeAbilityType;
    public EnergyType productEnergyType; // 자원 생산 타입 
    //public List<EnergyType> productEnergyTypeList =new List<EnergyType>(); // 자원 생산 타입 
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

public class TreeData
{

    public TreeType treeType;
    public AbTreeBase treePrefab; 
    
    public TreeAbilityType treeAbilityType;
    public EnergyType productEnergyType; // 자원 생산 타입 
   // public List<EnergyType> productEnergyTypeList =new List<EnergyType>(); // 자원 생산 타입 
    public TreeLevel maxLevel; // 최대 레벨 
    public TreeLevel curLevel; // 현재 레벨

    public List<TreeByLevelSO> treeByLevelList = new List<TreeByLevelSO>(); // 레벨에 따른 능력치 
    
    public void CopySOData(TreeDataSO _treeDataSO)
    {
        this.treeType = _treeDataSO.treeType; 
        this.treePrefab = _treeDataSO.treePrefab; 
        this.treeAbilityType = _treeDataSO.treeAbilityType; 
        this.productEnergyType = _treeDataSO.productEnergyType; 
        //this.productEnergyTypeList = _treeDataSO.productEnergyTypeList; 
        this.maxLevel = _treeDataSO.maxLevel; 
        this.curLevel = _treeDataSO.curLevel;
        this.treeByLevelList = _treeDataSO.treeByLevelList;
    }

}