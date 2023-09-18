using Unity.VisualScripting;
using UnityEngine; 

public class AttackModule : AbBaseModule
{
    private AIConditions aiConditions;

    protected virtual void Start()
    {
        aiConditions ??= mainModule.GetModule<AIConditions>(ModuleType.AICondition);
    }

    /// <summary>
    /// 광역 공격 
    /// </summary>
    public void AttackWideArea()
    {
        
    }
    /// <summary>
    /// 단일 근접 공격 
    /// </summary>
    public void MeleeAttack()
    {
        // 타겟이 있는가
        // 때릴려하는데 죽는다면 
        // 타겟 지정 했어 글너데 죽없어 
        // 그러면 다시 찾아야해 
        
        if (aiConditions.Target == null)
        {
            Debug.Log("공격가능한 타겟이 없습니다");
            return; 
        }
        // 데미지 주기 
        IDamagable _targetDamagable = aiConditions.Target.GetComponent<IDamagable>();
        if (_targetDamagable != null)
        {
            var _moveType  = _targetDamagable.MoveType;
            _targetDamagable.Damaged(mainModule.UnitDataSO.GetAttackAmount(_moveType));
        }
    }
}