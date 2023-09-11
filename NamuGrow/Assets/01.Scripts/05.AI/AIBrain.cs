using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class AIBrain<T>   where T : AbMainModule
{
    private T owner = null; 
    [SerializeField] private AIDataSO aiDataSO;
    private Dictionary<StateType, State<T>> _stateDic = new Dictionary<StateType, State<T>>();


    private Vector3 targetPosition; 
    private Transform target; 
    private State<T> curState;
    private State<T> prevState; 
    
    private IdleState<T> idleState;
    private ChaseState<T> chaseState;
    private PatrolState<T> patrolState;
    private AttackState<T> attackState;

    private bool isCanChase; 
    private bool isReachDestination;
    private bool isCanAttack; 
    private bool isControl; 
    
    
    private Vector3 targetDir; 
    #region 프로퍼티

    public Vector3 TargetDir
    {
        get
        {
            if (target != null & owner != null)
            {
                targetDir = target.position - owner.transform.position;
                return targetDir; 
            }
            Debug.LogError($"target : {target} owner : {owner} 입니다");
            return Vector3.zero; 
        }
    }
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
    }

    public AIDataSO AiDataSo => aiDataSO; 
    
    #endregion

    public void InitAIDataSO(AIDataSO _aiDataSO)
    {
        this.aiDataSO = _aiDataSO; 
    }
    public void InitOwner(T _owner)
    {
        owner = _owner;
    }
    
    public void Start()
    {
        idleState = new IdleState<T>();
        chaseState = new ChaseState<T>();
        attackState = new AttackState<T>(); 
        patrolState = new PatrolState<T>(); 
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
    public State<T> GetState(StateType _stateType)
    {
        State<T> _state = null; 
        if (_stateDic.ContainsKey(_stateType) == false)
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

    protected void AddState(State<T> _newState)
    {
        if (_stateDic.ContainsKey(_newState.stateType) == false)
        {
            _newState.Init(owner, this);
            _stateDic.Add(_newState.stateType, _newState);
        }
    }
    #endregion
    
    private void CheckChaseCondition()
    {
        
    }

    #region 찾기 
    public void SearchForAttackTarget()
    {
        var _target = SearchForTarget(aiDataSO.attackRadius, aiDataSO.attackAngle);
        if (_target != null)
        {
            isCanAttack = true; 
        }
        else
        {
            isCanAttack = false; 
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
            isCanChase = true; 
        }
        else
        {
            isCanChase = false; 
        }

        Target = _target; 
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
                float distanceToPlayer = Vector3.Distance(col.transform.position, owner.transform.position);

                // 시야각 내에 플레이어가 있고 감지 범위 내에 있다면 적을 목록에 추가합니다.
                if (distanceToPlayer <= _findRadius && IsFieldOfView(col.transform, _findAngle))
                {
                    nearbyEnemies.Add(col.transform);
                }
            }

            Transform selectedTarget = SelectTarget(nearbyEnemies.ToArray());
            return selectedTarget; 
        }

        return null; 
    }

    /// <summary>
    /// 유닛의 시야 안에 있는가 
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    private bool IsFieldOfView(Transform enemy, float _viewAngle)
    {
        Vector3 directionToPlayer = owner.transform.position - enemy.position;
        float angleToPlayer = Vector3.Angle(enemy.forward, directionToPlayer);

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