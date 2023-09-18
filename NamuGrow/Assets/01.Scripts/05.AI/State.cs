    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public AbMainModule owner;
    public AIBrain aiBrain; 
    public abstract StateType StateType { get;  }
    
    public abstract StateType PositiveType { get; }
    public abstract StateType NagativeType { get; }

    public virtual void Init(AbMainModule _owner, AIBrain _aiBrain)
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