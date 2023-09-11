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

    public void ClearMove()
    {
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
    
    
    public bool CheckReachDestination()
    {
        if(navMeshAgent.remainingDistance < 0.1f)
        {
        
            return false;
        }
        return true; 
    }
    
}
