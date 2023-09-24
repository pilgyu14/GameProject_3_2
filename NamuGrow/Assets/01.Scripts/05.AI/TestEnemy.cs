using System;
using System.Collections;
using System.Collections.Generic;
using LSystem;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : AbMainModule, IClickUnit, IDamagable,IPoolable
{
    [SerializeField] private AIDataSO aiDataSO;
    [SerializeField]
    private AIBrain aiBrain;

    private AIConditions aiCondition; 
    
    private AIMoveModule aiMoveModule;

    private AttackModule attackModule; 
    
    private UnitAniamtion unitAnimation; 
    
    // 플레이어 타입이면 
    // 명령을 받는다 
    // 
    // 살짝 ai 모드 
    // 
    private GameObject selectMark;

    [SerializeField]
    private TeamType teamType; 
    
    // 프로퍼티 
    public AIBrain AIBrain => aiBrain;

    
    protected override void Awake()
    {
        base.Awake();
        InitModules(); 

        //aiBrain = GetComponent<AIBrain<TestEnemy>>(); 
    }

    protected override void InitModules()
    {
        aiMoveModule = GetComponent<AIMoveModule>();
        AddModule(ModuleType.AIMove, aiMoveModule);
        attackModule = GetComponent<AttackModule>(); 
        AddModule(ModuleType.Attack, attackModule);
        aiBrain = GetComponent<AIBrain>();
        AddModule(ModuleType.AI, aiBrain);
        aiCondition = GetComponent<AIConditions>();
        AddModule(ModuleType.AICondition,aiCondition);
        unitAnimation = GetComponentInChildren<UnitAniamtion>(); 
        AddModule(ModuleType.Animation, unitAnimation);
        
        base.InitModules();
    }
    protected override  void Start()
    {
        base.Start();
        InitHp(); 
        //gameObject.layer =LayerMask.NameToLayer(aiDataSO.layerMask.ToString()); 
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

    private void InitHp()
    {
        MaxHealth = UnitDataSO.hp;
        CurHealth = MaxHealth;
        IsDied = false; 
    }
    public void Damaged(float _damageAmount)
    {
        Debug.Log("피격 : " + _damageAmount);
        float _curHp = CurHealth;
        _curHp -= _damageAmount;
        CurHealth = Mathf.Clamp(_curHp, 0, MaxHealth);
        if (CurHealth <= 0)
        {
            Die();
        }
    }

    public MoveType MoveType => UnitDataSO.moveType; 
    public void Die()
    {
        PoolManager.Instance.Push(gameObject);
        // 삭제 
    }

    [field: SerializeField] public bool IsDied { get; set; } = false;
    [field: SerializeField] public float MaxHealth { get; set; } 
    [field: SerializeField] public float CurHealth { get; set; }
    #endregion

    public void Reset()
    {
        InitHp(); 
    }
}