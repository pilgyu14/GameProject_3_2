using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override StateType StateType => StateType.Attack;
    
    public override StateType PositiveType { get; }
    public override StateType NagativeType => StateType.Chase;

    protected AIMoveModule aiMoveModule = null; 
    protected AIConditions aiCondition = null;
    protected AttackModule attackModule = null; 
    public override void Enter()
    {
        aiMoveModule ??= owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        aiCondition ??= owner.GetModule<AIConditions>(ModuleType.AICondition);
        attackModule ??= owner.GetModule<AttackModule>(ModuleType.Attack);
        aiMoveModule.ClearMove(); 
            
            
    }

    public override void Update()
    {
        Debug.Log("AttackState..");
        bool _isCanAttack = aiCondition.IsCanAttack; 
        aiBrain.SearchForAttackTarget();
        attackModule.MeleeAttack(_isCanAttack, owner.UnitDataSO.attackSpeed);

        if (_isCanAttack== false)
        {
            aiBrain.ChangeState(NagativeType);
        }
        // 타겟 지정하고 
        // 지상인지 공중인지 체크 
        // 모션 나오고 
        // 데미지 입히기 
        
    }

    public override void Exit()
    {
    }
}