using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    [SerializeField]
    private TeamType teamType = TeamType.None;
    private BoxCollider collider;
    private SkillSO skillSO;

    public BoxCollider Collider => collider; 
    private void Awake()
    {
        collider = GetComponent<BoxCollider>(); 
    }

    private void Start()
    {
        ActiveCollider(false); 
    }

    public void Init(SkillSO _skillSO)
    {
        this.skillSO = _skillSO; 
    }
    public void ActiveCollider(bool _isActive)
    {
        collider.enabled = _isActive; 
    }
    private void OnTriggerEnter(Collider other)
    {
        // 우리팀이 아니면 피해 입히기 
        if (other.GetComponent<ITeam>().TeamType != teamType)
        {
            other.GetComponent<IDamagable>().Damaged(skillSO.damage);
        }
    }
}
