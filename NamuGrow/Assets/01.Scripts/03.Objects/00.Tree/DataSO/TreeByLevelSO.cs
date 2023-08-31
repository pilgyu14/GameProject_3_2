using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 레벨에 따른 나무 데이터 
/// </summary>
[CreateAssetMenu(menuName = "SO/Tree/TreeByLevelSO")]
public class TreeByLevelSO : ScriptableObject
{
    public TreeLevel level; // 레벨 
    public GameObject changeTree; // 변할 나무 
    public Vector3 scale; // 크기 
    public int consumeEnergy; // 소비 에너지양

    public int addTroops; // 추가 병력수  
    public int productAmount; // 생산 에너지양 
    public EnergyIntDic produceAmountDic = new EnergyIntDic();

    public int attackPower;
    public int hp; 
    
    // 강화에 필요한 에너지 
    public EnergyIntDic needEnergyDic = new EnergyIntDic();
}
