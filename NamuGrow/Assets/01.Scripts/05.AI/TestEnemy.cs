using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    public override StateType StateType => StateType.Attack;
    
    public override StateType PositiveType { get; }
    public override StateType NagativeType => StateType.Chase;

    protected AIMoveModule aiMoveModule = null; 
    protected AIConditions aiCondition = null;
    protected AttackModule attackModule = null; 
    public override void Enter()
    {
        aiMoveModule ??= owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        aiCondition ??= owner.GetModule<AIConditions>(ModuleType.AICondition);
        attackModule ??= owner.GetModule<AttackModule>(ModuleType.Attack);
        aiMoveModule.ClearMove(); 
        
        attackModule.MeleeAttack();
        
    }

    public override void Update()
    {
        Debug.Log("AttackState..");
        aiBrain.SearchForAttackTarget();
        if (aiCondition.IsCanAttack == false)
        {
            aiBrain.ChangeState(NagativeType);
        }
        // 타겟 지정하고 
        // 지상인지 공중인지 체크 
        // 모션 나오고 
        // 데미지 입히기 
        
    }

    public override void Exit()
    {
    }
}


public class ChaseState : State
{
    public override StateType StateType => StateType.Chase;
    
    public override StateType PositiveType => StateType.Attack;
    public override StateType NagativeType => StateType.Idle;

    private AIMoveModule aiMoveModule;
    private AIConditions aiCondition; 

    public override void Enter()
    {
        aiMoveModule = owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        aiCondition = owner.GetModule<AIConditions>(ModuleType.AICondition);
        aiMoveModule.SetSpeed(aiBrain.AiDataSo.chaseSpeed);
    }

    public override void Update()
    {
        Debug.Log("ChaseState..");
        //aiMoveModule.MoveDir(aiBrain.TargetDir * Time.deltaTime);
        aiMoveModule.MovePosition(aiCondition.Target.position);
        
        aiBrain.SearchForChaseTarget();
        aiBrain.SearchForAttackTarget();
        if (aiCondition.IsCanAttack)
        {
            aiBrain.ChangeState(PositiveType);
        }

        if (aiCondition.IsCanChase == false)
        {
            aiBrain.ChangeState(NagativeType);

        }
    }

    public override void Exit()
    {
    }
}


public class IdleState : State
{
    public override StateType StateType => StateType.Idle;

    public override StateType PositiveType => StateType.Chase;
    public override StateType NagativeType => StateType.Idle;

    private AIMoveModule aiMoveModule;
    private AIConditions aiCondition; 
    public override void Enter()
    {
        aiMoveModule ??= owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        aiCondition ??= owner.GetModule<AIConditions>(ModuleType.AICondition);
        aiMoveModule.SetSpeed(aiBrain.AiDataSo.idleSpeed);
    }

    public override void Update()
    {
        // 거리 체크 
        // 추적 스테이트 변경 
        Debug.Log("IdleSate..");
        aiBrain.SearchForChaseTarget();
        if (aiCondition.Target != null && aiCondition.IsCanChase)
        {
            //aiMoveModule.MoveDir(aiBrain.TargetDir);
            aiBrain.ChangeState(PositiveType);
        }
    }

    public override void Exit()
    {
    }
}


public class PatrolState : State
{
    public override StateType StateType => StateType.Patrol; 
    
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

public class TestEnemy : AbMainModule, IClickUnit, IDamagable
{
    [SerializeField] private AIDataSO aiDataSO;
    [SerializeField]
    private AIBrain aiBrain;

    private AIConditions aiCondition; 
    
    private AIMoveModule aiMoveModule;
    
    private GameObject selectMark;  
    // 프로퍼티 
    public AIBrain AIBrain => aiBrain;

    
    protected override void Awake()
    {
        base.Awake();
        aiMoveModule = GetComponent<AIMoveModule>();
        AddModule(ModuleType.AIMove, aiMoveModule);
        aiBrain = GetComponent<AIBrain>();
        AddModule(ModuleType.AI, aiBrain);
        aiCondition = GetComponent<AIConditions>();
        AddModule(ModuleType.AICondition,aiCondition);
   
        //aiBrain = GetComponent<AIBrain<TestEnemy>>(); 
    }

    protected override  void Start()
    {
        base.Start();
        aiBrain.InitAIDataSO(aiDataSO);
        aiBrain.Start();
    }

    private void Update()
    {
        aiBrain.Update();
    }
   
    private void OnDrawGizmos()
    {
        if (aiBrain.AiDataSo == null) return; 
        // 범위를 그립니다. (원 형태)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aiBrain.AiDataSo.chaseViewRadius);
        // 시야각을 그립니다. (부채꼴 모양)
        DrawCircularSector(Color.green, aiBrain.AiDataSo.chaseViewAngle, aiBrain.AiDataSo.chaseViewRadius);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aiBrain.AiDataSo.attackRadius);
        DrawCircularSector(Color.green, aiBrain.AiDataSo.attackAngle, aiBrain.AiDataSo.attackRadius);


        // 플레이어를 발견한 경우, 플레이어 쪽으로 선을 그립니다.
        if (aiCondition.Target != null)
        {   
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, aiCondition.Target.position);
        }
    }

    private void DrawCircularSector(Color _color, float _viewAngle, float _radius)
    {
        Gizmos.color = _color;
        Vector3 forward = transform.forward;
        Quaternion leftRotation = Quaternion.AngleAxis(-_viewAngle * 0.5f, Vector3.up);
        Quaternion rightRotation = Quaternion.AngleAxis(_viewAngle * 0.5f, Vector3.up);
        Vector3 leftRayDirection = leftRotation * forward;
        Vector3 rightRayDirection = rightRotation * forward;
        Gizmos.DrawLine(transform.position, transform.position + leftRayDirection * _radius);
        Gizmos.DrawLine(transform.position, transform.position + rightRayDirection * _radius);
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

    #region  IDamaable 구현
    public void Damaged(float _damageAmount)
    {
        throw new NotImplementedException();
    }

    public MoveType MoveType { get; }
    public void Die()
    {
        // 삭제 
    }

    [field: SerializeField] public bool IsDied { get; set; } = false;
    [field: SerializeField] public float MaxHealth { get; set; } 
    [field: SerializeField] public float CurHealth { get; set; }
    #endregion
   
}