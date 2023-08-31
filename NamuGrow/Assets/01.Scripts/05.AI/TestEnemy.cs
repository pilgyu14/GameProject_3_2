using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum StateType
{
    Patrol,
    Move,
    Attack, 
    Died, 
}

public abstract class State<T>
{
    public T owner; 
    
    public virtual void Init(T owner)
    {
        this.owner = owner;
    }
    public abstract void Enter(); 
    public abstract void Stay(); 
    public abstract void Exit(); 
}

public class EnemyDataSO
{
    private string name;
        
    private GoodsType goodsType;
    private float goodsCount; 
    
    public TogetherTime togetherTime;

}

/// <summary>
/// AI 상태 변화에 필요한 데이터 모음 
/// </summary>
[CreateAssetMenu(menuName = "SO/AI/AIDataSO")]
public class AIDataSO : ScriptableObject 
{
    public float viewAngle;
    public float viewRadius;

    public float patrolRadius; 
    
    public float attackAngle;
    public float attackRadius; 
}
public class PatrolState : State<TestEnemy>
{
    public override void Enter()
    {
    }

    public override void Stay()
    {
    }

    public override void Exit()
    {
    }
}

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private AIDataSO aiDataSO; 
    
    private Dictionary<Type, State<TestEnemy>> _stateDic = new Dictionary<Type, State<TestEnemy>>();
    private StateType curStateType;

    private State<TestEnemy> curState; 
    private PatrolState patrolState;

    private void Awake()
    {
        patrolState.Init(this);
    }

    private void Update()
    {
        if (curState != null)
        {
            curState.Stay(); 
        }
    }

    public void ChangeState()
    {
        
    }
}
