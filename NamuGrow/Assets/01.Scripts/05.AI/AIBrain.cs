using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class AIBrain : AbBaseModule
{
    #region FSM

    private AbMainModule owner = null; 
    [SerializeField] private AIDataSO aiDataSO;
    private Dictionary<StateType, State> _stateDic = new Dictionary<StateType, State>();


    private State curState;
    private State prevState; 
    
    private IdleState idleState;
    private ChaseState chaseState;
    private PatrolState patrolState;
    private AttackState attackState;

    protected AIConditions aiConditions; 
    public AIDataSO AiDataSo => aiDataSO; 
    
    #endregion

    public void InitAIDataSO(AIDataSO _aiDataSO)
    {
        this.aiDataSO = _aiDataSO; 
    }
   
    public override void InitMainModule(AbMainModule _mainModule)
    {
        base.InitMainModule(_mainModule);
        this.owner = _mainModule; 
    }

    public void Start()
    {
        idleState = new IdleState();
        chaseState = new ChaseState();
        attackState = new AttackState(); 
        patrolState = new PatrolState(); 
        AddState(idleState);
        AddState(chaseState);
        AddState(attackState);
        AddState(patrolState);
        ChangeState(StateType.Idle);
        
    }
    public void Update()
    {
        RunAI();
        
    }

    #region  FSM
    private void RunAI()
    {
        if (owner != null)
        {
            curState.Update();
        }
    }
    
    /// <summary>
    /// 상태 받아오기 
    /// </summary>
    /// <param name="_stateType"></param>
    /// <returns></returns>
    public State GetState(StateType _stateType)
    {
        State _state = null; 
        if (_stateDic.ContainsKey(_stateType) == true)
        {
            _state = _stateDic[_stateType];
        }
        else
        {
            Debug.LogError(Enum.GetName(typeof(StateType),_stateType) + "상태가 없습니다 ");
        }
        return _state; 
    }
    
    /// <summary>
    /// 상태 변경 
    /// </summary>
    /// <param name="_stateType"></param>
    public void ChangeState(StateType _stateType)
    {
        if (curState != null)
        {
            curState.Exit();
            prevState = curState; 
        }
        var _newState = GetState(_stateType);
        curState = _newState; 
        _newState.Enter(); 
    }

    protected void AddState(State _newState)
    {
        if (_stateDic.ContainsKey(_newState.StateType) == false)
        {
            _newState.Init(owner, this);
            _stateDic.Add(_newState.StateType, _newState);
        }
    }
    #endregion
    

    #region 찾기 
    public void SearchForAttackTarget()
    {
        var _target = SearchForTarget(aiDataSO.attackRadius, aiDataSO.attackAngle);
        if (_target != null)
        {
            aiConditions.IsCanAttack = true; 
        }
        else
        {
            aiConditions.IsCanAttack = false;   
        }
    }
    
    /// <summary>
    /// 추적 타겟 찾기 
    /// </summary>
    public void SearchForChaseTarget()
    {
        var _target = SearchForTarget(aiDataSO.chaseViewRadius, aiDataSO.chaseViewAngle);
        if (_target != null)
        {
            aiConditions.IsCanChase = true; 
        }
        else
        {
            aiConditions.IsCanChase = false; 
        }
        aiConditions.Target = _target; 
    }

    /// <summary>
    /// 목표 찾기 
    /// </summary>
    /// <param name="_findRadius">찾을 범위</param>
    /// <param name="_findAngle">찾을 시야각</param>
    public Transform SearchForTarget(float _findRadius, float _findAngle)
    {
        Collider[] targets = Physics.OverlapSphere(owner.transform.position, _findRadius, aiDataSO.layerMask);

        List<Transform> nearbyEnemies = new List<Transform>(); 
        if (targets.Length > 0)
        {
            foreach (var col in targets)
            {
                // 플레이어와 적의 거리를 계산합니다.
                //float distanceToPlayer = Vector3.Distance(col.transform.position, owner.transform.position);

                // 시야각 내에 플레이어가 있고 감지 범위 내에 있다면 적을 목록에 추가합니다.
                // + 공격 가능한 적이라면 
                if (/*distanceToPlayer <= _findRadius &&*/ IsFieldOfView(col.transform, _findAngle) && IsCanTargeting(col.transform) && CheckNotDied(col.transform))
                {
                    nearbyEnemies.Add(col.transform);
                }
            }

            Transform selectedTarget = SelectTarget(nearbyEnemies.ToArray());
            return selectedTarget; 
        }

        return null; 
    }

    private bool CheckNotDied(Transform _enemy)
    {
        return _enemy.GetComponent<IDamagable>().IsDied; 
    }
    /// <summary>
    /// 지상 타입인지 공중 타입인지 파악해서 확인 
    /// </summary>
    /// <returns></returns>
    private bool IsCanTargeting(Transform _enemy)
    {
        IDamagable _damagable = _enemy.GetComponent<IDamagable>();
        if (_damagable != null)
        {
            if ((owner.UnitDataSO.groundAttack > 0 && _damagable.MoveType == MoveType.ground)
                || (owner.UnitDataSO.airAttack > 0 && _damagable.MoveType == MoveType.air))
            {
                return true; 
            }
        }
        Debug.Log("공격할 수 없는 대상 : " + _enemy.name);
        return false; 
    }
    /// <summary>
    /// 유닛의 시야 안에 있는가 
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    private bool IsFieldOfView(Transform enemy, float _viewAngle)
    {
        Vector3 directionToPlayer = enemy.position - owner.transform.position;
        float angleToPlayer = Vector3.Angle(owner.transform.forward, directionToPlayer);

        // 시야각 내에 플레이어가 있는지 확인합니다.
        if (angleToPlayer < _viewAngle * 0.5f)
        {
            // 시야각 내에 플레이어가 있으면 true 반환
            return true;
        }

        // 시야각 내에 플레이어가 없으면 false 반환
        return false;
    }
    
    /// <summary>
    /// 우선 순위 타겟 선택 
    /// </summary>
    /// <param name="targets"></param>
    /// <returns></returns>
    private Transform SelectTarget(Transform[] targets)
    {
        Transform bestTarget = null;
        float highestPriority = float.MinValue;

        foreach (Transform target in targets)
        {
            float priority = CalculatePriority(target.gameObject);
            if (priority > highestPriority)
            {
                highestPriority = priority;
                bestTarget = target.transform;
            }
        }

        return bestTarget;
    }

    /// <summary>
    /// 우선 순위 계산 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private float CalculatePriority(GameObject target)
    {
        float priority = 0; 
        foreach (var _layerMask in aiDataSO.layerMaskPriority.Dictionary)
        {
            if (target.layer == _layerMask.Value)
            {
                priority = _layerMask.Key; 
            }
        }
        return priority;
    }
    #endregion

}