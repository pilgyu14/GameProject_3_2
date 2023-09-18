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

    public Transform testTrm;

    [ContextMenu("테스트 무브")]
    public void MoveTest()
    {
        navMeshAgent.SetDestination(testTrm.position);
    }

    public int testValue = 1;
    public Vector3 movePos; 
    [ContextMenu("테스트 무브2")]
    public void MoveTest2()
    {
        navMeshAgent.Move(movePos);
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
