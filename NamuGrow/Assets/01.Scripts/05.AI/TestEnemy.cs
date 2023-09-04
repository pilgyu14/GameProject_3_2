using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyDataSO
{
    private string name;
        
    private GoodsType goodsType;
    private float goodsCount; 
    
    public TogetherTime togetherTime;

}


public class AttackState<T> : State<T> where T : AbMainModule
{
    public override StateType PositiveType { get; }
    public override StateType NagativeType { get; }

    public override void Enter()
    {
    }

    public override void Update()
    {
        Debug.Log("AttackState..");
    }

    public override void Exit()
    {
    }
}


public class ChaseState<T> : State<T> where T : AbMainModule
{
    public override StateType PositiveType { get; }
    public override StateType NagativeType { get; }

    public override void Enter()
    {
    }

    public override void Update()
    {
        Debug.Log("ChaseState..");

    }

    public override void Exit()
    {
    }
}


public class IdleState<T> : State<T> where T : AbMainModule
{
    public override StateType PositiveType => StateType.Chase;
    public override StateType NagativeType => StateType.Idle;

    public override void Enter()
    {
    }

    public override void Update()
    {
        // 거리 체크 
        // 추적 스테이트 변경 
        Debug.Log("IdleSate..");
    }

    public override void Exit()
    {
    }
}


public class PatrolState<T> : State<T> where T : AbMainModule
{
    public override StateType PositiveType { get; }
    public override StateType NagativeType { get; }

    public override void Enter()
    {
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

public class TestEnemy : AbMainModule
{
    [SerializeField] private AIBrain<TestEnemy> aiBrain;
    
    private NavMeshAgent navMeshAgent; 
    
    private void Awake()
    {
    }

    private void Start()
    {
        aiBrain.InitOwner(this);
        aiBrain.Start();
    }

    private void Update()
    {
        aiBrain.Update();
    }


}
