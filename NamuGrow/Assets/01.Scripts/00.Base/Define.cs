
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region  Enum

public enum TreeSystem
{
    Produce, // 자원 생산 
    Wall, // 벽 
    Attack, // 공격 
}

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
public enum TreeAbilityType
{
    None = 0,  
    Product = 1 << 0, // 생산 
    Defend = 1 << 1, // 방어
    Attack = 1 << 2, // 공격 
    
}

public enum TreeType
{
    BirchTree,
    Test,
    Test2,
    
}

public interface IDamagable
{
    public void Damaged(float _damageAmount);
    void Die(); 
    float MaxHealth { get; set; }
    float CurHealth { get; set; }
}

/// <summary>
/// 클릭되는 유닛인가 (아군인가) 
/// </summary>
public interface IClickUnit
{
    public bool IsClickUnit { get; set; }
    public void ClickUnit();
    public void CancelClickUnit(); 
}

public interface IAIAgent
{
    
}

public enum StateType
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Damaged, 
    Battle, 
    Died, 
}

public enum AnimationTriggerType
{
    
}
#endregion

public class Define 
{


}
