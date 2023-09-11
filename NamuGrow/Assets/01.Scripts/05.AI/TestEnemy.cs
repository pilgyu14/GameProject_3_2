using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState<T> : State<T> where T : AbMainModule
{
    public override StateType PositiveType { get; }
    public override StateType NagativeType => StateType.Chase;

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
    public override StateType PositiveType => StateType.Attack;
    public override StateType NagativeType => StateType.Idle;

    private AIMoveModule aiMoveModule;

    public override void Enter()
    {
        aiMoveModule = owner.GetModule<AIMoveModule>(ModuleType.AIMove);
    }

    public override void Update()
    {
        Debug.Log("ChaseState..");
        aiMoveModule.MoveDir(aiBrain.TargetDir);
        
        aiBrain.SearchForChaseTarget();
        aiBrain.SearchForAttackTarget();
        if (aiBrain.IsCanAttack)
        {
            aiBrain.ChangeState(PositiveType);
        }

        if (aiBrain.IsCanChase == false)
        {
            aiBrain.ChangeState(NagativeType);

        }
    }

    public override void Exit()
    {
    }
}


public class IdleState<T> : State<T> where T : AbMainModule
{
    public override StateType PositiveType => StateType.Chase;
    public override StateType NagativeType => StateType.Idle;

    private AIMoveModule aiMoveModule;

    public override void Enter()
    {
        aiMoveModule = owner.GetModule<AIMoveModule>(ModuleType.AIMove);
    }

    public override void Update()
    {
        // 거리 체크 
        // 추적 스테이트 변경 
        Debug.Log("IdleSate..");
        aiBrain.SearchForChaseTarget();
        if (aiBrain.Target != null && aiBrain.IsCanChase)
        {
            //aiMoveModule.MoveDir(aiBrain.TargetDir);
            aiBrain.ChangeState(PositiveType);
        }
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

public class TestEnemy : AbMainModule, IClickUnit
{
    [SerializeField] private AIDataSO aiDataSO;
     private AIBrain<TestEnemy> aiBrain;

    private AIMoveModule aiMoveModule;

    private GameObject selectMark;  
    // 프로퍼티 
    public AIBrain<TestEnemy> AIBrain => aiBrain;

    
    protected override void Awake()
    {
        base.Awake();
        aiMoveModule = GetComponent<AIMoveModule>();
        AddModule(ModuleType.Move, aiMoveModule);

        aiBrain = new AIBrain<TestEnemy>(); 
        //aiBrain = GetComponent<AIBrain<TestEnemy>>(); 
    }

    protected override  void Start()
    {
        aiBrain.InitOwner(this);
        aiBrain.InitAIDataSO(aiDataSO);
        aiBrain.Start();
    }

    private void Update()
    {
        aiBrain.Update();
    }

    [field:SerializeField]public bool IsClickUnit { get; set; }
    public void ClickUnit()
    {
        selectMark.SetActive(true);
    }

    public void CancelClickUnit()
    {
        selectMark.SetActive(false);
    }
}