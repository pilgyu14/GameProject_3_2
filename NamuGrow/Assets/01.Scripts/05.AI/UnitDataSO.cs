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
    
    public MoveType moveType;
    public float moveSpeed;

    // UI Data 
}
