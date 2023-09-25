using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        aiMoveModule.ClearMove();
    }
}
