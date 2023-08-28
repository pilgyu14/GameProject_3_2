
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region  Enum 

public enum TreeLevel
{
    tree_level_first, // 묘목 
    tree_level_second, // 애기 나무
    tree_level_third, // 청소년 나무 
    tree_level_fourth, // 성인 나무 
    tree_level_fifth, // 고목 
}
    
public enum EnergyType
{
    None, // 없음 
    Sap, // 수액 
    SweetSap, // 단 수액 
    Acorn, // 도토리
    Kiwi, // 키위 
        
    //Water, // 물  
    //SunEnergy, // 빛 에너지 
    // Nutrient, // 양분
}

/// <summary>
/// 나무 역할 
/// </summary>
[Flags]
public enum TreeType
{
    None = 0,  
    Product = 1 << 0, // 생산 
    Defend = 1 << 1, // 방어
    Attack = 1 << 2, // 공격 
    
}


#endregion

public class Define 
{


}
