
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : AbMonster 
{
    public T owner;
    public AIBrain<T> aiBrain; 
    public StateType stateType;
    
    public abstract StateType PositiveType { get; }
    public abstract StateType NagativeType { get; }

    public virtual void Init(T _owner, AIBrain<T> _aiBrain)
    {
        this.owner = _owner;
        this.aiBrain = _aiBrain; 
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

    public virtual void AnimationTriggerEvent(AnimationTriggerType _animationTriggerType)
    {
    }
}