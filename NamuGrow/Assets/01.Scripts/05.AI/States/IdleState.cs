using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override StateType StateType => StateType.Idle;

    public override StateType PositiveType => StateType.Chase;
    public override StateType NagativeType => StateType.Idle;

    private AIMoveModule aiMoveModule;
    private AIConditions aiCondition;

    private AIConditions AICondition
    {
        get
        {
            if (aiCondition == null)
            {
                aiCondition = owner.GetModule<AIConditions>(ModuleType.AICondition);
            }

            return aiCondition; 
        }
    }
    public override void Enter()
    {
        aiMoveModule ??= owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        aiCondition ??= owner.GetModule<AIConditions>(ModuleType.AICondition);
        aiMoveModule.SetSpeed(aiBrain.AiDataSo.idleSpeed);
    }

    public override void Update()
    {
        // 거리 체크 
        // 추적 스테이트 변경 
        Debug.Log("IdleSate..");
        aiBrain.SearchForChaseTarget();
        if (AICondition.Target != null && AICondition.IsCanChase)
        {
            //aiMoveModule.MoveDir(aiBrain.TargetDir);
            aiBrain.ChangeState(PositiveType);
        }
    }

    public override void Exit()
    {
    }
}

