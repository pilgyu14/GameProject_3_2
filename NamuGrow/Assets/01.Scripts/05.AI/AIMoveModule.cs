using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements; 
public class AIMoveModule : AbBaseModule
{
    private NavMeshAgent navMeshAgent;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>(); 
    }

    [ContextMenu("멈춰")]
    public void ClearMove()
    {
        navMeshAgent.SetDestination(mainModule.transform.position);
    }

    public void MoveDir(Vector3 _dir)
    {
        navMeshAgent.Move(_dir); 
    }

    public void MovePosition(Vector3 _pos)
    {
        navMeshAgent.SetDestination(_pos); 
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
    
}
