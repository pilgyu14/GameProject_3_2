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

    public float hp; 
    public float groundAttack;
    public float airAttack;
    public float attackSpeed;
    
    // 광역 공격 
    
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
