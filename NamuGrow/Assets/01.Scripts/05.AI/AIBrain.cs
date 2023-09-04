using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class AIBrain<T>  where T : AbMonster
{
    private T owner = null; 
    [SerializeField] private AIDataSO aiDataSO;
    private Dictionary<StateType, State<T>> _stateDic = new Dictionary<StateType, State<T>>();
    
    
    private Transform target; 
    private State<T> curState;
    private State<T> prevState; 
    
    private IdleState<T> idleState;
    private ChaseState<T> chaseState;
    private PatrolState<T> patrolState;
    private AttackState<T> attackState;

    #region 프로퍼티

    private bool isCanChase; 
    public bool IsCanChase
    {
        get
        {
            return isCanChase;
        }
    }
    
    
    #endregion
    
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
    private void RunAI()
    {
        if (owner != null)
        {
            curState.Update();
        }
    }

    private void CheckTarget()
    {
        
    }
    private void CheckChaseCondition()
    {
     //   Physics.
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
}