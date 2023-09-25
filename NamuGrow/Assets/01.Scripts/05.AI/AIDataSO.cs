using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// AI 상태 변화에 필요한 데이터 모음 
/// </summary>
[CreateAssetMenu(menuName = "SO/AI/AIDataSO")]
public class AIDataSO : ScriptableObject
{
    [Header("자기 자신 레이어 마스크")]
    public LayerMask myLayerMask;
    [Header("타겟 레이어 마스크")]
    public LayerMask layerMask;
    [Header("우선순위 타겟 레이어 마스크 ( Key의 숫자가 높을 수록 높은 우선순위)")]
    public IntLayerMaskDic layerMaskPriority;
    
    public float idleSpeed;
    public float chaseSpeed; 
    
    public float chaseViewAngle;
    public float chaseViewRadius;

    public float patrolRadius; 
    
    public float attackAngle;
    public float attackRadius; 
}