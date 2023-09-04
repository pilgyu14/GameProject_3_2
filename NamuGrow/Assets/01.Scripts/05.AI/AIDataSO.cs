using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// AI 상태 변화에 필요한 데이터 모음 
/// </summary>
[CreateAssetMenu(menuName = "SO/AI/AIDataSO")]
public class AIDataSO : ScriptableObject
{
    [Header("우선순위 타겟 레이어 마스크")]
    public IntLayerMaskDic layerMask;

    public float idleSpeed;
    public float chaseSpeed; 
    
    public float viewAngle;
    public float viewRadius;

    public float patrolRadius; 
    
    public float attackAngle;
    public float attackRadius; 
}