using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandState : State
{
    public override StateType StateType => StateType.Command;
    public override StateType PositiveType => StateType.Idle; 
    public override StateType NagativeType { get; }

    private AIMoveModule moveModule; 
    
    public override void Enter()
    {
        moveModule ??= owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        moveModule.ClearMove(); 
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
