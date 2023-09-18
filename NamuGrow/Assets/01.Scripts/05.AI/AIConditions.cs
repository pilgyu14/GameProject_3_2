using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConditions : AbBaseModule
{
    private Vector3 targetPosition; 
    private Transform target; 

    [SerializeField]
    private bool isCanChase; 
    [SerializeField]
    private bool isReachDestination;
    [SerializeField]
    private bool isCanAttack; 
    [SerializeField]
    private bool isControl;
    [SerializeField] 
    private bool isBasicAttackCool; 
    public Transform Target
    {
        get => target;
        set => target = value; 
    }
    public Vector3 TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    }

    public bool IsCanAttack
    {
        get => isCanAttack;
        set => isCanAttack = value; 
    }
    /// <summary>
    /// 유저가 컨트롤 중인가 
    /// </summary>
    public bool IsControl
    {
        get => isControl;
        set => isControl = value; 
    }

    public bool IsReachDestination
    {
        get => isReachDestination;
        set => isReachDestination = value; 
    }
    public bool IsCanChase
    {
        get
        {
            return isCanChase;
        }
        set
        {
            isCanChase = value; 
        }
    }
    
    public bool IsBasicAttackCool
    {
        get => isBasicAttackCool;
        set => isBasicAttackCool = value; 
    }
}
