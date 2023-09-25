using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSO")]
public class SkillSO : ScriptableObject
{
    public float damage;
    public float coolTime; 
    public GameObject effectParent;
}
