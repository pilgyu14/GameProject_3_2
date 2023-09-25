using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit/UnitDataSO")]
public class UnitDataSO : ScriptableObject
{
    private string name;

    private GoodsType goodsType;
    private float goodsCount;

    public TogetherTime togetherTime;

    [Space(10), Header("유닛 스탯")]
    public float hp; 
    [Header("*값이 0이면 공격할 수 없음")]
    public float groundAttack;
    public float airAttack;
    
    public float attackSpeed;
    
    // 광역 공격 
    [Header("이동 타입")]
    public MoveType moveType;
    public float moveSpeed;

    // UI Data

    public float GetAttackAmount(MoveType _moveType)
    {
        if (_moveType == MoveType.air)
        {
            return airAttack; 
        }
        else if (_moveType == MoveType.ground)
        {
            return groundAttack; 
        }

        return 0; 
    }
}
