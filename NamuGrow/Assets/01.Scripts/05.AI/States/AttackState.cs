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
    protected SkillModule skillModule = null; 
    
    public override void Enter()
    {
        aiMoveModule ??= owner.GetModule<AIMoveModule>(ModuleType.AIMove);
        aiCondition ??= owner.GetModule<AIConditions>(ModuleType.AICondition);
        attackModule ??= owner.GetModule<AttackModule>(ModuleType.Attack);
        skillModule ??= owner.GetModule<SkillModule>(ModuleType.Skill);
        aiMoveModule.ClearMove(); 
            
            
    }

    public override void Update()
    {
        // 공격 상태에서 
        // 스킬 사용중에는 막아야해 
        // 
        Debug.Log("AttackState..");
        aiBrain.SearchForSkillTarget();
        if (CheckCanSkill() == true)
        {         
            return; 
        }

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

    /// <summary>
    /// 스킬 사용가능 여부 체크 
    /// </summary>
    /// <returns></returns>
    private bool CheckCanSkill()
    {
        bool _isCanSkill = aiCondition.IsCanSkill;
        if (_isCanSkill == true)
        {
            skillModule.PlayEffect(); 
            //aiBrain.ChangeState(StateType.Skill);
            return true;
        }
        else
        {
            
        }

        return false;
    }
}