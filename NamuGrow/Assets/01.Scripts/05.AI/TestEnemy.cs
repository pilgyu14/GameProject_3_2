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


public class AttackState<T> : State<T> where T : AbMonster
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


public class ChaseState<T> : State<T> where T : AbMonster
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


public class IdleState<T> : State<T> where T : AbMonster
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
    }

    public override void Exit()
    {
    }
}


public class PatrolState<T> : State<T> where T : AbMonster
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

public class TestEnemy : AbMonster
{
    [SerializeField] private AIBrain<TestEnemy> aiBrain; 

    private void Awake()
    {
    }

    private void Update()
    {
    }


}
