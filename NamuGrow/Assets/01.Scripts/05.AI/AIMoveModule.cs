using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements; 
public class AIMoveModule : AbBaseModule, IUpdateObj
{
    private NavMeshAgent navMeshAgent;

    private UnitAniamtion unitAniamtion; 
    
    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>(); 
    }

    private void OnEnable()
    {
        UpdateManager.Instance.AddUpdateObj(this);
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.RemoveUpdateObj(this);
    }

    protected override void Start()
    {
        base.Start();
        unitAniamtion = mainModule.GetModule<UnitAniamtion>(ModuleType.Animation);
    }

    public void LookTarget(Transform _target)
    {
    }
    [ContextMenu("멈춰")]
    public void ClearMove()
    {
        navMeshAgent.SetDestination(mainModule.transform.position);
    }

    public Vector3 pos; 
    [ContextMenu("이동")]
    public void TestMove()
    {
        navMeshAgent.SetDestination(pos); 
        unitAniamtion.PlayMoveAnim(navMeshAgent.velocity.sqrMagnitude);
    }
public void MovePosition(Vector3 _pos)
    {                                                            
        navMeshAgent.SetDestination(_pos); 
        unitAniamtion.PlayMoveAnim(navMeshAgent.velocity.sqrMagnitude);
        //navMeshAgent.Move(_pos); 
        //navMeshAgent.    T
    }

    public void SetSpeed(float _speed)
    {
        navMeshAgent.speed = _speed; 
    }
    
    public bool CheckReachDestination()
    {
        if(navMeshAgent.remainingDistance < 0.1f)
        {
        
            return false;
        }
        return true; 
    }

    public void OnUpdate()
    {
        //  이동 중이라면 
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            unitAniamtion.PlayMoveAnim(navMeshAgent.velocity.sqrMagnitude);
        }
    }

    public void OnLateUpdate()
    {
    }

    public void OnFixedUpdate()
    {
    }
}
