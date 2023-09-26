
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAniamtion : AbBaseModule
{
    [SerializeField]
    protected Animator animator;

    protected readonly int moveStr = Animator.StringToHash("IdleAndRun") ;
    protected readonly int damagedHash = Animator.StringToHash("Damaged");
    protected readonly int dieHash = Animator.StringToHash("Die");
    protected readonly int skillHash = Animator.StringToHash("Skill");
    protected readonly int attackHash = Animator.StringToHash("Attack");
    protected readonly int attackSpeedHash = Animator.StringToHash("AttackSpeed");
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); 
    }
    
    public void PlayMoveAnim(float _speed)
    {
        animator.SetFloat(moveStr, _speed);
    }

    public void SetAttackAnimSpeed(float _value)
    {
        animator.SetFloat(attackSpeedHash, _value);
    }
    public void PlayAttackAnim(bool _isAttack)
    {
        animator.SetBool(attackHash, _isAttack);
    }    
    public void PlaySkillAnim(bool _isActive)
    {
        animator.SetBool(skillHash, _isActive);
    }    
    public void PlayDieAnim()
    {
        animator.SetTrigger(dieHash);
    }
    public void PlayDamagedAnim()
    {
        animator.SetTrigger(damagedHash);
    }
}

