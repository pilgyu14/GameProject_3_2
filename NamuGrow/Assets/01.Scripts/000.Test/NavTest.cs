using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform testTrm; 
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    [ContextMenu("이동 테스트")]
    public void MoveTest()
    {
        agent.SetDestination(testTrm.position);
    }
}
