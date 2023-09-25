using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
